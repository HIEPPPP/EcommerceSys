using Ecommerce.API.Data;
using Ecommerce.API.Models.Domain;
using Ecommerce.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.API.Repositories.Implementation
{
    public class ImageRepository : IImageRepository
    {
        private readonly EcomsysHiepddContext dbContext;

        public ImageRepository(EcomsysHiepddContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Image> Upload(Image image)
        {
            // Add Image to the Image table DATABASE
            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();
            return image;
        }
    }
}
