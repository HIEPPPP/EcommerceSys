using Ecommerce.API.Data;
using Ecommerce.API.Models.Domain;
using Ecommerce.API.Repositories.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly EcomsysHiepddContext dbContext;

        public ProductRepository(EcomsysHiepddContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> DeleteAsync(int id)
        {
            var exitstProduct = await dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);  
            if (exitstProduct == null) 
            { 
                return null;
            }
            dbContext.Products.Remove(exitstProduct);
            await dbContext.SaveChangesAsync();
            return exitstProduct;
        }

        public async Task<List<Product>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool? isAscending = null, int pageNumber = 1, int pageSize = 10)
        {
            var products = dbContext.Products.AsQueryable();
            
            // Filter
            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    products = products.Where(p => p.Name.Contains(filterQuery));
                }
            }

            // Sort
            if(string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(sortBy.Equals("Price", StringComparison.OrdinalIgnoreCase))
                {
                    products = (isAscending ?? true) ? products.OrderBy(p => p.Price) : products.OrderByDescending(p => p.Price);
                } 
            }

            // Pagination
            var skipResult = (pageNumber - 1) * pageSize;

            return await Task.FromResult(products.Skip(skipResult).Take(pageSize).ToList());
        }

        public async Task<Product?> GetAsync(int id)
        {
            var exitstProduct = await dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (exitstProduct == null)
            {
                return null;
            }
            return exitstProduct;
        }

        public async Task<Product?> UpdateAsync(int id, Product product)
        {
            var existProduct = await dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (existProduct == null)
            {
                return null;
            }

            existProduct.Name = product.Name;
            existProduct.Desc = product.Desc;
            existProduct.Sku = product.Sku;
            existProduct.Price = product.Price;
            existProduct.Discount = product.Discount;
            existProduct.Inventory = product.Inventory;
            existProduct.CategoryId = product.CategoryId;
            existProduct.DiscountId = product.DiscountId;

            await dbContext.SaveChangesAsync();
            return existProduct;
        }
    }
}
