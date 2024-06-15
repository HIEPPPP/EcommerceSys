using Ecommerce.API.Data;
using Ecommerce.API.Models.Domain;
using Ecommerce.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly EcomsysHiepddContext dbContext;
        private readonly IUserPaymentRepository userPaymentRepository;
        private readonly IUAddressRepository userAddressRepository;

        public UserRepository(EcomsysHiepddContext dbContext, IUserPaymentRepository userPaymentRepository, IUAddressRepository userAddressRepository)
        {
            this.dbContext = dbContext;
            this.userPaymentRepository = userPaymentRepository;
            this.userAddressRepository = userAddressRepository;
        }
        public async Task<User> CreateAsync(User user)
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User?> DeleteAsync(int id)
        {
            var existUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);   
            if (existUser == null)
            {
                return null;
            }
            await userAddressRepository.DeleteUAdressByUserIdAsync(id);
            await userPaymentRepository.DeleteUserPaymentByUserId(id);
            dbContext.Users.Remove(existUser);
            await dbContext.SaveChangesAsync(); 
            return existUser;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await dbContext.Users.ToListAsync();
        }

        public async Task<User?> GetAsync(int id)
        {
            var existUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (existUser == null)
            {
                return null;
            }
            return existUser;
        }

        public async Task<User?> UpdateAsync(int id, User user)
        {
            var existUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (existUser == null)
            {
                return null;
            }
            existUser.Username = user.Username;
            existUser.Password = user.Password;
            existUser.FirstName = user.FirstName;
            existUser.LastName = user.LastName;
            existUser.Telephone = user.Telephone;   
            
            await dbContext.SaveChangesAsync();
            return existUser;
        }
    }
}
