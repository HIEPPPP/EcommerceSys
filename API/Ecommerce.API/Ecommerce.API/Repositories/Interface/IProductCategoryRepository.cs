using Ecommerce.API.Models.Domain;

namespace Ecommerce.API.Repositories.Interface
{
    public interface IProductCategoryRepository
    {
        Task<List<ProductCategory>> GetAllAsync();
        Task<ProductCategory?> GetAsync(int id);
        Task<ProductCategory> CreateAsync(ProductCategory productCategory);
        Task<ProductCategory?> UpdateAsync(int id, ProductCategory productCategory);
        Task<ProductCategory?> DeleteAsync(int id);

    }
}
