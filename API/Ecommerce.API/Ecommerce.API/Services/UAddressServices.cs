using AutoMapper;
using Ecommerce.API.Models.Domain;
using Ecommerce.API.Models.DTO;
using Ecommerce.API.Repositories.Interface;

namespace Ecommerce.API.Services
{
    public class UAddressServices
    {
        private readonly IUAddressRepository uAddressRepository;
        private readonly IMapper mapper;

        public UAddressServices(IUAddressRepository uAddressRepository, IMapper mapper)
        {
            this.uAddressRepository = uAddressRepository;
            this.mapper = mapper;
        }
        public async Task<CreateUpdateUAddressRequestDto> CreateAsync(CreateUpdateUAddressRequestDto dto)
        {
            var uAdressDomain = mapper.Map<UserAddress>(dto);
            await uAddressRepository.CreateAsync(uAdressDomain);
            return mapper.Map<CreateUpdateUAddressRequestDto>(uAdressDomain);
        }

        public async Task<List<UAddressDto>> GetAllAsync()
        {
            var userAddress = await uAddressRepository.GetAllAsync();
            return mapper.Map<List<UAddressDto>>(userAddress);
        }

        public async Task<UAddressDto?> GetAsync(int id)
        {
            var existUAdress = await uAddressRepository.GetAsync(id);
            if (existUAdress == null)
            {
                return null;
            }
            return mapper.Map<UAddressDto?>(existUAdress);
        }

        public async Task<UAddressDto?> DeleteAsync(int id)
        {
            var existUAdress = await uAddressRepository.DeleteAsync(id);
            if (existUAdress == null)
            {
                return null;
            }
            return mapper.Map<UAddressDto?>(existUAdress);
        }

        public async Task<UAddressDto?> UpdateAsync(int id, CreateUpdateUAddressRequestDto dto)
        {
            var uAdressDomain = mapper.Map<UserAddress>(dto);
            var existUAdress = await uAddressRepository.UpdateAsync(id, uAdressDomain);
            if (existUAdress == null)
            {
                return null;
            }
            return mapper.Map<UAddressDto>(existUAdress);
        }
    }
}
