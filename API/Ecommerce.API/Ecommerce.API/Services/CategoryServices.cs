using AutoMapper;
using Ecommerce.API.Models.Domain;
using Ecommerce.API.Models.DTO;
using Ecommerce.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Services
{
    public class CategoryServices
    {
        private readonly IProductCategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CategoryServices(IProductCategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public async Task<List<ProductCategory>> GetCategories()
        {
            return await categoryRepository.GetAllAsync();
        }

        public async Task<ProductCategory?> GetCategory(int id)
        {
            var existCategory = await categoryRepository.GetAsync(id);
            if (existCategory == null)
            {
                return null;
            }
            return existCategory;
    }

        public async Task<CreateCategoryRequestDto> CreateCategory(CreateCategoryRequestDto createCategoryRequestDto)
        {
            var categoryDomail = mapper.Map<ProductCategory>(createCategoryRequestDto);
            await categoryRepository.CreateAsync(categoryDomail);
            mapper.Map<CreateCategoryRequestDto>(categoryDomail);
            return createCategoryRequestDto;
        }

        public async Task<ProductCategory?> DeleteCategoryAsync(int id)
        {

            var existCategory = await categoryRepository.DeleteAsync(id);
            if (existCategory == null)
            {
                return null;
            }
            return existCategory;
        }

        [HttpPut("{id}")]
        public async Task<UpdateCategoryRequestDto?> UpdateCategory(int id, UpdateCategoryRequestDto updateCategoryRequestDto)
        {
            var categoryDomain = mapper.Map<ProductCategory>(updateCategoryRequestDto);

            var existCategory = await categoryRepository.UpdateAsync(id, categoryDomain);
            if (existCategory == null)
            {
                return null;
            }
            return mapper.Map<UpdateCategoryRequestDto>(existCategory);
        }
    }
}
