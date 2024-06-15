using System;
using System.Collections.Generic;

namespace Ecommerce.API.Models.Domain;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Desc { get; set; }

    public string Sku { get; set; } = null!;

    public int CategoryId { get; set; }

    public int InventoryId { get; set; }

    public decimal Price { get; set; }

    public int? DiscountId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? Image { get; set; }

    public virtual CartItem? CartItem { get; set; }

    public virtual ProductCategory Category { get; set; } = null!;

    public virtual Discount? Discount { get; set; }

    public virtual ProductInventory Inventory { get; set; } = null!;

    public virtual OrderItem? OrderItem { get; set; }
}
