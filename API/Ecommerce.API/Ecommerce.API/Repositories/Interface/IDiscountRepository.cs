using Ecommerce.API.Models.Domain;

namespace Ecommerce.API.Repositories.Interface
{
    public interface IDiscountRepository
    {
        Task<List<Discount>> GetAllAsync();
        Task<Discount?> GetAsync(int id);
        Task<Discount> CreateAsync(Discount discount);
        Task<Discount?> UpdateAsync(int id, Discount discount);
        Task<Discount?> DeleteAsync(int id);
    }
}
