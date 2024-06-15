using Ecommerce.API.Data;
using Ecommerce.API.Models.Domain;
using Ecommerce.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories.Implementation
{
    public class UserPaymentRepository : IUserPaymentRepository
    {
        private readonly EcomsysHiepddContext dbContext;

        public UserPaymentRepository(EcomsysHiepddContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<UserPayment> CreateAsync(UserPayment userPayment)
        {
            await dbContext.UserPayments.AddAsync(userPayment);
            await dbContext.SaveChangesAsync();
            return userPayment;
        }

        public async Task<UserPayment?> DeleteAsync(int id)
        {
            var existUPayment = await dbContext.UserPayments.FindAsync(id);
            if (existUPayment == null)
            {
                return null;
            }
            dbContext.UserPayments.Remove(existUPayment);
            await dbContext.SaveChangesAsync();
            return existUPayment;
        }

        public async Task<List<UserPayment>> GetAllAsync()
        {
            return await dbContext.UserPayments.ToListAsync();
        }

        public async Task<UserPayment?> GetAsync(int id)
        {
            var existUPayment = await dbContext.UserPayments.FindAsync(id);
            if (existUPayment == null)
            {
                return null;
            }
            return existUPayment;
        }

        public async Task<List<UserPayment>?> DeleteUserPaymentByUserId(int userId)
        {
            var existUPayments = await dbContext.UserPayments.Where(u => u.UserId == userId).ToListAsync();
            if (existUPayments == null)
            {
                return null;
            }
            dbContext.UserPayments.RemoveRange(existUPayments);
            await dbContext.SaveChangesAsync();
            return existUPayments;
        }

        public async Task<UserPayment?> UpdateAsync(int id, UserPayment userPayment)
        {
            var existUPayment = await dbContext.UserPayments.FindAsync(id);
            if (existUPayment == null)
            {
                return null;
            }
            existUPayment.PaymentType = userPayment.PaymentType;
            existUPayment.Provider = userPayment.Provider;
            existUPayment.AccountNo = userPayment.AccountNo;
            existUPayment.Expiry = userPayment.Expiry;

            await dbContext.SaveChangesAsync();
            return existUPayment;
        }
    }
}
