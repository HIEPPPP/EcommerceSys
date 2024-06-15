using Ecommerce.API.Data;
using Ecommerce.API.Models.Domain;
using Ecommerce.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories.Implementation
{
    public class ProductInventoryRepository : IProductInventoryRepository
    {
        private readonly EcomsysHiepddContext dbContext;

        public ProductInventoryRepository(EcomsysHiepddContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ProductInventory> CreateAsync(ProductInventory productInventory)
        {
            await dbContext.ProductInventories.AddAsync(productInventory);
            await dbContext.SaveChangesAsync();
            return productInventory;
        }

        public async Task<ProductInventory?> DeleteAsync(int id)
        {
            var existPInventory = await dbContext.ProductInventories.FirstOrDefaultAsync(x => x.Id == id);
            if (existPInventory == null)
            {
                return null;
            }
            dbContext.ProductInventories.Remove(existPInventory);   
            await dbContext.SaveChangesAsync();
            return existPInventory;
        }

        public async Task<List<ProductInventory>> GetAllAsync()
        {
            return await Task.FromResult(dbContext.ProductInventories.ToList());
        }

        public async Task<ProductInventory?> GetAsync(int id)
        {
            var existPInventory = await dbContext.ProductInventories.FirstOrDefaultAsync(x => x.Id == id);
            if(existPInventory == null)
            {
                return null;
            }
            return existPInventory;
        }

        public async Task<ProductInventory?> UpdateAsync(int id, ProductInventory productInventory)
        {
            var existPInventory = await dbContext.ProductInventories.FirstOrDefaultAsync(x => x.Id == id);
            if (existPInventory == null)
            {
                return null;
            }

            existPInventory.Quantity = productInventory.Quantity;
            await dbContext.SaveChangesAsync();
            return existPInventory;
        }
    }
}
