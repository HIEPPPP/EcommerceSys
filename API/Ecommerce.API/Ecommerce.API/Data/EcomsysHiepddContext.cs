using System;
using System.Collections.Generic;
using Ecommerce.API.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Data;

public partial class EcomsysHiepddContext : IdentityDbContext
{
    public EcomsysHiepddContext()
    {
    }

    public EcomsysHiepddContext(DbContextOptions<EcomsysHiepddContext> options)
        : base(options)
    {
    }

    public DbSet<ExtendedIdentityUser> ExtendedIdentityUsers { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<PaymentDetail> PaymentDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductInventory> ProductInventories { get; set; }

    public virtual DbSet<ShoppingSession> ShoppingSessions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAddress> UserAddresses { get; set; }

    public virtual DbSet<UserPayment> UserPayments { get; set; }
    public virtual DbSet<Image> Images { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=5.104.82.65;Database=ecomsys_hiepdd;User Id=ecomsys;Password=yq21oA7A6526;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Console.WriteLine("OnModelCreating");
        base.OnModelCreating(modelBuilder);

        //var adminId = "d5fff3f1-a5eb-452d-96df-379d65504a05";
        //var writerId = "1dce7166-de48-4957-8b94-0ac779721364";

        //var roles = new List<IdentityRole>
        //{
        //    new IdentityRole
        //    {
        //        Id = adminId,
        //        ConcurrencyStamp = adminId,
        //        Name = "Admin",
        //        NormalizedName = "Admin".ToUpper(),
        //    },
            //new IdentityRole
            //{
            //    Id = writerId,
            //    ConcurrencyStamp = writerId,
            //    Name = "Writer",
            //    NormalizedName = "Writer".ToUpper(),

            //}
        //};

        //modelBuilder.Entity<IdentityRole>().HasData(roles);
        

        //Seed data for category product
        //var categorys = new List<ProductCategory>
        //{
        //    new ProductCategory
        //    {
        //        Id = 1,
        //        Name = "Accessories",
        //        Desc = "Phụ kiện unisex",
        //    },
        //    new ProductCategory
        //    {
        //        Id = 2,
        //        Name = "Fitness",
        //        Desc = "",
        //    },
        //    new ProductCategory
        //    {
        //        Id = 3,
        //        Name = "Clothing",
        //        Desc = "Quần áo unisex",
        //    },
        //    new ProductCategory
        //    {
        //        Id = 4,
        //        Name = "Electronics",
        //        Desc = "Thiết bị điện tử",
        //    },
        //};

        // Seed category product to the database
        //modelBuilder.Entity<ProductCategory>().HasData(categorys);

        // Seed data for discount product
        //var discounts = new List<Discount>
        //{
        //    new Discount
        //    {
        //        Id = 1,
        //        Name = "Giảm 20%",
        //        Desc = "Chương trình giảm giá",
        //        DiscountPercent = 20,
        //        Active = false,
        //    },
        //    new Discount
        //    {
        //        Id = 2,
        //        Name = "Giảm 25%",
        //        Desc = "Chương trình giảm giá các ngày lễ trong năm",
        //        DiscountPercent = 25,
        //        Active = false,
        //    },
        //    new Discount
        //    {
        //        Id = 3,
        //        Name = "Giảm 50%",
        //        Desc = "Hàng tồn kho",
        //        DiscountPercent = 50,
        //        Active = false,
        //    }
        //};
        //modelBuilder.Entity<Discount>().HasData(discounts);


        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.ToTable("cart_item");

            entity.HasIndex(e => e.ProductId, "UQ_cart_item_product_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.ModifiedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("modified_at");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.SessionId).HasColumnName("session_id");

            entity.HasOne(d => d.Product).WithOne(p => p.CartItem)
                .HasForeignKey<CartItem>(d => d.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_cart_item_product");

            entity.HasOne(d => d.Session).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.SessionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cart_item_shopping_session");
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.ToTable("discount");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.CreatedAt)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("created_at");
            entity.Property(e => e.DeleteAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("delete_at");
            entity.Property(e => e.Desc)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("desc");
            entity.Property(e => e.DiscountPercent)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("discount_percent");
            entity.Property(e => e.ModifiedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("modified_at");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.ToTable("order_details");

            entity.HasIndex(e => e.PaymentId, "UQ_order_details_payment_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.ModifiedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("modified_at");
            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("total");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Payment).WithOne(p => p.OrderDetail)
                .HasForeignKey<OrderDetail>(d => d.PaymentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_order_details_payment_details");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.ToTable("order_items");

            entity.HasIndex(e => e.ProductId, "UQ_order_items_product_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.ModifiedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("modified_at");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_order_items_order_details");

            entity.HasOne(d => d.Product).WithOne(p => p.OrderItem)
                .HasForeignKey<OrderItem>(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_order_items_product");
        });

        modelBuilder.Entity<PaymentDetail>(entity =>
        {
            entity.ToTable("payment_details");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.ModifiedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("modified_at");
            entity.Property(e => e.Provider)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("provider");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("product");

            entity.HasIndex(e => e.CategoryId, "UQ_product_category_id").IsUnique();

            entity.HasIndex(e => e.InventoryId, "UQ_product_inventory_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Desc)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("desc");
            entity.Property(e => e.DiscountId).HasColumnName("discount_id");
            entity.Property(e => e.Image)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.InventoryId).HasColumnName("inventory_id");
            entity.Property(e => e.ModifiedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("modified_at");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("price");
            entity.Property(e => e.Sku)
                .HasMaxLength(100)
                .HasColumnName("SKU");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_product_product_category");

            entity.HasOne(d => d.Discount).WithMany(p => p.Products)
                .HasForeignKey(d => d.DiscountId)
                .HasConstraintName("FK_product_discount");

            entity.HasOne(d => d.Inventory).WithOne(p => p.Product)
                .HasForeignKey<Product>(d => d.InventoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_product_product_inventory");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.ToTable("product_category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DeleteAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("delete_at");
            entity.Property(e => e.Desc)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("desc");
            entity.Property(e => e.ModifiedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("modified_at");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ProductInventory>(entity =>
        {
            entity.ToTable("product_inventory");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DeleteAt)
                .HasColumnType("datetime")
                .HasColumnName("delete_at");
            entity.Property(e => e.ModifiedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("modified_at");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
        });

        modelBuilder.Entity<ShoppingSession>(entity =>
        {
            entity.ToTable("shopping_session");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.ModifiedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("modified_at");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("total");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.ShoppingSessions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_shopping_session_user");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_NewTable");

            entity.ToTable("user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.ModifiedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("modified_at");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Telephone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telephone");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<UserAddress>(entity =>
        {
            entity.ToTable("user_address");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddressLine1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("address_line1");
            entity.Property(e => e.AddressLine2)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("address_line2");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("country");
            entity.Property(e => e.Mobile)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("mobile");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("postal_code");
            entity.Property(e => e.Telephone)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("telephone");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserAddresses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user_address_user");
        });

        modelBuilder.Entity<UserPayment>(entity =>
        {
            entity.ToTable("user_payment");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountNo).HasColumnName("account_no");
            entity.Property(e => e.Expiry)
                .HasColumnType("string")
                .HasColumnName("expiry");
            entity.Property(e => e.PaymentType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("payment_type");
            entity.Property(e => e.Provider)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("provider");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserPayments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user_payment_user");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
