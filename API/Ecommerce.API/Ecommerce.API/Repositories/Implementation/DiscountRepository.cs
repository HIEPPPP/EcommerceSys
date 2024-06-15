using Ecommerce.API.Data;
using Ecommerce.API.Models.Domain;
using Ecommerce.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories.Implementation
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly EcomsysHiepddContext dbContext;

        public DiscountRepository(EcomsysHiepddContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Discount> CreateAsync(Discount discount)
        {
            await dbContext.Discounts.AddAsync(discount);
            await dbContext.SaveChangesAsync();
            return discount;
        }

        public async Task<Discount?> DeleteAsync(int id)
        {
            var product = from p in dbContext.Products
                          where p.DiscountId == id
                          select p;
            
            var existDiscount = await dbContext.Discounts.FindAsync(id);
            if (existDiscount == null)
            {
                return null;
            }
            dbContext.Discounts.Remove(existDiscount);
            product.ToList().ForEach(x => x.DiscountId = null);
            await dbContext.SaveChangesAsync();
            return existDiscount;
        }

        public async Task<List<Discount>> GetAllAsync()
        {
            return await Task.FromResult(dbContext.Discounts.ToList());
        }

        public async Task<Discount?> GetAsync(int id)
        {
            var existDiscount = await dbContext.Discounts.FindAsync(id);
            if (existDiscount == null)
            {
                return null;
            }
            return existDiscount;
        }

        public async Task<Discount?> UpdateAsync(int id, Discount discount)
        {
            var existDiscount = await dbContext.Discounts.FindAsync(id);
            if (existDiscount == null)
            {
                return null;
            }
            existDiscount.DiscountPercent = discount.DiscountPercent;
            existDiscount.Active = discount.Active;
            existDiscount.Name = discount.Name;
            existDiscount.Desc = discount.Desc;

            await dbContext.SaveChangesAsync(); 
            return existDiscount;
        }
    }
}
