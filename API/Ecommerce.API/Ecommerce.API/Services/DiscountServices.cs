using AutoMapper;
using Ecommerce.API.Models.Domain;
using Ecommerce.API.Models.DTO;
using Ecommerce.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Services
{
    public class DiscountServices
    {
        private readonly IDiscountRepository discountRepository;
        private readonly IMapper mapper;

        public DiscountServices(IDiscountRepository discountRepository, IMapper mapper)
        {
            this.discountRepository = discountRepository;
            this.mapper = mapper;
        }

        public async Task<List<DiscountDto>> GetAllDiscountAsync()
        {
            var discountDomain = await discountRepository.GetAllAsync();
            return mapper.Map<List<DiscountDto>>(discountDomain);
        }

        public async Task<DiscountDto?> GetDiscountAsync(int id)
        {
            var existDiscount = await discountRepository.GetAsync(id);
            if (existDiscount == null)
            {
                return null;
            }
            return mapper.Map<DiscountDto>(existDiscount);
        }

        public async Task<CreateUpdateDiscountRequestDto> CreateDiscountAsync(CreateUpdateDiscountRequestDto createDiscountRequestDto)
        {
            var discountDomail = mapper.Map<Discount>(createDiscountRequestDto);
            await discountRepository.CreateAsync(discountDomail);
            mapper.Map<CreateUpdateDiscountRequestDto>(discountDomail);
            return createDiscountRequestDto;
        }

        public async Task<DiscountDto?> DeleteDiscountAsync(int id)
        {

            var existDiscount = await discountRepository.DeleteAsync(id);
            if (existDiscount == null)
            {
                return null;
            }
            return mapper.Map<DiscountDto>(existDiscount);
        }

        [HttpPut("{id}")]
        public async Task<CreateUpdateDiscountRequestDto?> UpdateDiscountAsync(int id, CreateUpdateDiscountRequestDto updateDiscountRequestDto)
        {
            var discountDomain = mapper.Map<Discount>(updateDiscountRequestDto);

            var existDiscount = await discountRepository.UpdateAsync(id, discountDomain);
            if (existDiscount == null)
            {
                return null;
            }
            return mapper.Map<CreateUpdateDiscountRequestDto>(existDiscount);
        }
    }
}
