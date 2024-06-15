using Ecommerce.API.Models.Domain;

namespace Ecommerce.API.Repositories.Interface
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image); 
    }
}
