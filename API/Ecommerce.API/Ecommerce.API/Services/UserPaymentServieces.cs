using AutoMapper;
using Ecommerce.API.Models.Domain;
using Ecommerce.API.Models.DTO;
using Ecommerce.API.Repositories.Interface;

namespace Ecommerce.API.Services
{
    public class UserPaymentServieces
    {
        private readonly IUserPaymentRepository userPaymentRepository;
        private readonly IMapper mapper;

        public UserPaymentServieces(IUserPaymentRepository userPaymentRepository, IMapper mapper)
        {
            this.userPaymentRepository = userPaymentRepository;
            this.mapper = mapper;
        }
        public async Task<UPaymentDto> CreateAsync(CreateUPaymentRequestDto dto)
        {
            var uPaymentDomain = mapper.Map<UserPayment>(dto);
            await userPaymentRepository.CreateAsync(uPaymentDomain);
            return mapper.Map<UPaymentDto>(uPaymentDomain);
        }

        public async Task<List<UPaymentDto>> GetAllAsync()
        {
            return mapper.Map<List<UPaymentDto>>(await userPaymentRepository.GetAllAsync());
        }


        public async Task<UPaymentDto?> GetAsync(int id)
        {
            var existUPayment = await userPaymentRepository.GetAsync(id);
            if (existUPayment == null)
            {
                return null;
            }
            return mapper.Map<UPaymentDto>(existUPayment);
        }

        public async Task<UPaymentDto?> DeleteAsync(int id)
        {
            var existUPayment = await userPaymentRepository.DeleteAsync(id);
            if (existUPayment == null)
            {
                return null;
            }
            return mapper.Map<UPaymentDto>(existUPayment);
        }

        public async Task<UpdateUPaymentRequestDto?> UpdateAsync(int id, UpdateUPaymentRequestDto dto)
        {
            var userPaymentDomain = mapper.Map<UserPayment>(dto);
            var existUPayment = await userPaymentRepository.UpdateAsync(id, userPaymentDomain);
            if (existUPayment == null)
            {
                return null;
            }
            return mapper.Map<UpdateUPaymentRequestDto>(existUPayment);
        }
    }
}
