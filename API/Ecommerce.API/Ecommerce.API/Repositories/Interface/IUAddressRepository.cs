using Ecommerce.API.Models.Domain;

namespace Ecommerce.API.Repositories.Interface
{
    public interface IUAddressRepository
    {
        Task<List<UserAddress>> GetAllAsync();
        Task<UserAddress?> GetAsync(int id);
        Task<List<UserAddress>?> DeleteUAdressByUserIdAsync(int userId);
        Task<UserAddress> CreateAsync(UserAddress userAddress);
        Task<UserAddress?> DeleteAsync(int id);
        Task<UserAddress?> UpdateAsync(int id, UserAddress userAddress);
    }
}
