using Ecommerce.API.Data;
using Ecommerce.API.Models.Domain;
using Ecommerce.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories.Implementation
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly EcomsysHiepddContext dbContext;

        public ProductCategoryRepository(EcomsysHiepddContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ProductCategory> CreateAsync(ProductCategory productCategory)
        {
            await dbContext.ProductCategories.AddAsync(productCategory);
            await dbContext.SaveChangesAsync();
            return productCategory;
        }

        public async Task<ProductCategory?> DeleteAsync(int id)
        {
            var existPCategory = await dbContext.ProductCategories.FirstOrDefaultAsync(c => c.Id == id);
            if (existPCategory == null)
            {
                return null;
            }
            dbContext.ProductCategories.Remove(existPCategory);
            await dbContext.SaveChangesAsync();
            return existPCategory;
        }

        public async Task<List<ProductCategory>> GetAllAsync()
        {
            return await dbContext.ProductCategories.ToListAsync();
        }

        public async Task<ProductCategory?> GetAsync(int id)
        {
            var existPCategory = await dbContext.ProductCategories.FirstOrDefaultAsync(c => c.Id == id);
            if (existPCategory == null)
            {
                return null;
            }
            return existPCategory;
        }

        public async Task<ProductCategory?> UpdateAsync(int id, ProductCategory productCategory)
        {
            var existPCategory = await dbContext.ProductCategories.FirstOrDefaultAsync(c => c.Id == id);
            if (existPCategory == null)
            {
                return null;
            }
            existPCategory.Name = productCategory.Name;
            existPCategory.Desc = productCategory.Desc;
            await dbContext.SaveChangesAsync();
            return existPCategory;
        }
    }
}
