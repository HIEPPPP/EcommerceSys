using Ecommerce.API.Models.Domain;

namespace Ecommerce.API.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetAsync(int id);
        Task<User> CreateAsync(User user);
        Task<User?> UpdateAsync(int id, User user);
        Task<User?> DeleteAsync(int id);

    }
}
