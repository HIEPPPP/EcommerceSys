using Ecommerce.API.Data;
using Ecommerce.API.Models.Domain;
using Ecommerce.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories.Implementation
{
    public class UserAddressRepository : IUAddressRepository
    {
        private readonly EcomsysHiepddContext dbContext;

        public UserAddressRepository(EcomsysHiepddContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<UserAddress> CreateAsync(UserAddress userAddress)
        {
            await dbContext.UserAddresses.AddAsync(userAddress);
            await dbContext.SaveChangesAsync();
            return userAddress; 
        }

        public async Task<UserAddress?> DeleteAsync(int id)
        {
            var existUAddress = await dbContext.UserAddresses.FirstOrDefaultAsync(u => u.Id == id);
            if (existUAddress == null)
            {
                return null;
            }
            dbContext.UserAddresses.Remove(existUAddress);
            await dbContext.SaveChangesAsync();
            return existUAddress;
        }

        public async Task<List<UserAddress>> GetAllAsync()
        {
            return await dbContext.UserAddresses.ToListAsync();
        }

        public async Task<UserAddress?> GetAsync(int id)
        {
            var existUAddress = await dbContext.UserAddresses.FirstOrDefaultAsync(u => u.Id == id);
            if (existUAddress == null)
            {
                return null;
            }
            return existUAddress;
        }

        public async Task<List<UserAddress>?> DeleteUAdressByUserIdAsync(int userId)
        {
            var existUAddresses = await dbContext.UserAddresses.Where(u => u.UserId == userId).ToListAsync();
            if (existUAddresses == null)
            {
                return null;
            }
            dbContext.UserAddresses.RemoveRange(existUAddresses);
            await dbContext.SaveChangesAsync();
            return existUAddresses;
        }

        public async Task<UserAddress?> UpdateAsync(int id, UserAddress userAddress)
        {
            var existUAddress = await dbContext.UserAddresses.FirstOrDefaultAsync(u => u.Id == id);
            if (existUAddress == null)
            {
                return null;
            }
            existUAddress.AddressLine1 = userAddress.AddressLine1;
            existUAddress.AddressLine2 = userAddress.AddressLine2;
            existUAddress.City = userAddress.City;
            existUAddress.PostalCode = userAddress.PostalCode;
            existUAddress.Country = userAddress.Country;
            existUAddress.Telephone = userAddress.Telephone;
            existUAddress.Mobile = userAddress.Mobile;

            await dbContext.SaveChangesAsync();
            return existUAddress;
        }
    }
}
