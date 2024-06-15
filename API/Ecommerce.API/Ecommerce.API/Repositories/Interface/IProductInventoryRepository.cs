using Ecommerce.API.Models.Domain;

namespace Ecommerce.API.Repositories.Interface
{
    public interface IProductInventoryRepository
    {
        Task<List<ProductInventory>> GetAllAsync();
        Task<ProductInventory?> GetAsync(int id);
        Task<ProductInventory> CreateAsync(ProductInventory productInventory);
        Task<ProductInventory?> UpdateAsync(int id, ProductInventory productInventory);
        Task<ProductInventory?> DeleteAsync(int id);

    }
}
