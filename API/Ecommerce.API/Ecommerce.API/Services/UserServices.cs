using AutoMapper;
using Ecommerce.API.Models.Domain;
using Ecommerce.API.Models.DTO;
using Ecommerce.API.Repositories.Interface;

namespace Ecommerce.API.Services
{
    public class UserServices
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserServices(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            var userDomain = await userRepository.GetAllAsync();
            return mapper.Map<List<UserDto>>(userDomain); 
        }

        public async Task<UserDto?> GetAsync(int id)
        {
            var existUser = await userRepository.GetAsync(id);
            if (existUser == null)
            {
                return null;
            }
            return mapper.Map<UserDto>(existUser);
        }

        public async Task<UserDto> CreateAsync(CreateUpdateUserRequestDto dto)
        {
            var userDomain = mapper.Map<User>(dto);
            await userRepository.CreateAsync(userDomain);
            return mapper.Map<UserDto>(userDomain);
        }

        public async Task<UserDto?> UpdateAsync(int id, CreateUpdateUserRequestDto dto)
        {
            var userDomain = mapper.Map<User>(dto);
            var existUser = await userRepository.UpdateAsync(id, userDomain);
            if (existUser == null)
            {
                return null;
            }
            return mapper.Map<UserDto>(userDomain);
        }

        public async Task<UserDto?> DeleteAsync(int id)
        {
            var existUser = await userRepository.DeleteAsync(id);
            if (existUser == null)
            {
                return null;
            }
            
            await userRepository.DeleteAsync(id);

            return mapper.Map<UserDto>(existUser);
        }
    }
}
