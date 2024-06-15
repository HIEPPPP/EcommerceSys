using Ecommerce.API.Models.Domain;

namespace Ecommerce.API.Repositories.Interface
{
    public interface IUserPaymentRepository
    {
        Task<UserPayment> CreateAsync(UserPayment userPayment); 
        Task<UserPayment?> UpdateAsync(int id, UserPayment userPayment);
        Task<UserPayment?> DeleteAsync(int id);
        Task<UserPayment?> GetAsync(int id);
        Task<List<UserPayment>> GetAllAsync();
        Task<List<UserPayment>?> DeleteUserPaymentByUserId(int userId);
    }
}
