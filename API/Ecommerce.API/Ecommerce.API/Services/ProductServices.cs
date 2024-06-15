using AutoMapper;
using Ecommerce.API.Data;
using Ecommerce.API.Models.Domain;
using Ecommerce.API.Models.DTO;
using Ecommerce.API.Repositories.Implementation;
using Ecommerce.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.ComponentModel;

namespace Ecommerce.API.Services
{
    public class ProductServices
    {
        private readonly IMapper mapper;
        private readonly IProductInventoryRepository productInventoryRepository;
        private readonly IProductRepository productRepository;
        private readonly IProductCategoryRepository productCategoryRepository;
        private readonly IDiscountRepository discountRepository;

        public ProductServices(IMapper mapper ,IProductInventoryRepository productInventoryRepository, 
            IProductRepository productRepository, IProductCategoryRepository productCategoryRepository, IDiscountRepository discountRepository  )
        {
            this.mapper = mapper;
            this.productRepository = productRepository;
            this.productCategoryRepository = productCategoryRepository;
            this.discountRepository = discountRepository;
            this.productInventoryRepository = productInventoryRepository;
        }
        public async Task<CreateProductRequestDto> CreateAsync(CreateProductRequestDto createProductRequestDto)
        {
           
            CreateProductInventoryRequestDto createProductInventoryRequestDto = new CreateProductInventoryRequestDto()
            {
                Quantity = createProductRequestDto.Quantity,    
            };
            // Create Product Inventory
            var productInventoryDomain = mapper.Map<ProductInventory>(createProductInventoryRequestDto);
            productInventoryDomain = await productInventoryRepository.CreateAsync(productInventoryDomain);

            // Create Product
            var productDomainModel = mapper.Map<Product>(createProductRequestDto);
            productDomainModel.InventoryId = productInventoryDomain.Id;
            productDomainModel = await productRepository.CreateAsync(productDomainModel);
           
            //Map Domain to Dto
            createProductRequestDto = mapper.Map<CreateProductRequestDto>(productDomainModel);
            return createProductRequestDto;
        }

        public async Task<List<ProductDto>> GetProductsAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool? isAscending = null, int pageNumber = 1, int pageSize = 10)
        {
            var products =  await productRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            var productInventory = await productInventoryRepository.GetAllAsync();
            var productCategory = await productCategoryRepository.GetAllAsync();
            var pDiscount = await discountRepository.GetAllAsync();

            var query = from p in products
                        join pInventory in productInventory on p.InventoryId equals pInventory.Id
                        join pCategory in productCategory on p.CategoryId equals pCategory.Id
                        join discount in pDiscount on p.DiscountId equals discount.Id into d
                        from di in d.DefaultIfEmpty()
                        select new ProductDto
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Desc = p.Desc,
                            Category = pCategory.Name,
                            CategoryId = p.CategoryId,
                            Sku = p.Sku,
                            DiscountId = p.DiscountId,
                            Quantity = pInventory.Quantity,
                            Price = p.Price,
                            DiscountPercent = (di != null) ? di.DiscountPercent : 0,
                            Image = p.Image,
                        };
            return await Task.FromResult(query.ToList());
            
        }

        public async Task<List<ProductDto>> GetProductAsync(int id)
        {
            var products = await productRepository.GetAllAsync();
            var productInventory = await productInventoryRepository.GetAllAsync();
            var productCategory = await productCategoryRepository.GetAllAsync();
            var pDiscount = await discountRepository.GetAllAsync();

            var query = from p in products
                        join pInventory in productInventory on p.InventoryId equals pInventory.Id
                        join pCategory in productCategory on p.CategoryId equals pCategory.Id
                        join discount in pDiscount on p.DiscountId equals discount.Id into d
                        from di in d.DefaultIfEmpty()
                        where p.Id == id
                        select new ProductDto
                        {
                            Name = p.Name,
                            Desc = p.Desc,
                            Category = pCategory.Name,
                            Quantity = pInventory.Quantity,
                            Price = p.Price,
                            DiscountPercent = (di != null) ? di.DiscountPercent : 0,
                            Image = p.Image,
                        };
            return await Task.FromResult(query.ToList());
        }

        public async Task<Product?> DeleteProductAsync(int id)
        {
            var existProducct = await productRepository.DeleteAsync(id);
            if(existProducct == null)
            {
                return null;
            }
            var existPInventory = await productInventoryRepository.DeleteAsync(existProducct.InventoryId);
            if(existPInventory == null)
            {
                return null;
            }
            return existProducct;
        }

        public async Task<UpdateProductRequestDto?> UpdateProductAsync(int id, UpdateProductRequestDto updateProductRequestDto)
        {
            // Update Product
            var productDomain = mapper.Map<Product>(updateProductRequestDto);
            var existProduct = await productRepository.UpdateAsync(id, productDomain);
            if(existProduct == null)
            {
                return null;
            }

            // Update Product Inventory
            var productInventoryDomain = mapper.Map<ProductInventory>(updateProductRequestDto);
            var existPInventory = await productInventoryRepository.GetAsync(existProduct.InventoryId);
            await productInventoryRepository.UpdateAsync(existPInventory.Id, productInventoryDomain);

            return new UpdateProductRequestDto
            {
                Id = existProduct.Id,
                Name = existProduct.Name,
                Desc = existProduct.Desc,
                Sku = existProduct.Sku,
                Price = existProduct.Price,
                Quantity = existPInventory.Quantity,
                CategoryId = existProduct.CategoryId,
                DiscountId = existProduct.DiscountId,
                Image = existProduct.Image,
            };
        }
    }
}
