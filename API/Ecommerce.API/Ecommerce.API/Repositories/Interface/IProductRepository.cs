using Ecommerce.API.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Repositories.Interface
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool? isAscending = null, int pageNumber = 1, int pageSize = 10);
        Task<Product?> GetAsync(int id);
        Task<Product> CreateAsync(Product product);
        Task<Product?> UpdateAsync(int id, Product product);
        Task<Product?> DeleteAsync(int id);
    }
}
