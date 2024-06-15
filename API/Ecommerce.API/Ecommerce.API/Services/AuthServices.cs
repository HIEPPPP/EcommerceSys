using Ecommerce.API.Models.Domain;
using Ecommerce.API.Models.DTO;
using Ecommerce.API.Repositories.Implementation;
using Ecommerce.API.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Data;


namespace Ecommerce.API.Services
{
    public class AuthServices
    {
        private readonly UserManager<ExtendedIdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthServices(UserManager<ExtendedIdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        public async Task<string> Register(RegisterRequestDto registerRequestDto)
        {
            var identityUser = new ExtendedIdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username,
            };
            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                return "User was registered! Please login.";
                // Add roles to this User
                //if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                //{
                //    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                //    if (identityResult.Succeeded)
                //    {
                //        return "User was registered! Please login.";
                //    }
                //}
            }
            return "Something went wrong";
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var response = new LoginResponseDto();
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);
            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (checkPasswordResult)
                {
                    //Get roles for user
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        //Create Token                   
                        response.JwtToken = tokenRepository.CreateToken(user);
                        response.RefreshToken = tokenRepository.CreateRefreshToken();

                        user.RefreshToken = response.RefreshToken;
                        user.RefreshTokenExpiry = DateTime.Now.AddHours(12);
                        await userManager.UpdateAsync(user);

                        return response;
                    }
                }
            }
            return response;
        }

        public async Task<LoginResponseDto> RefreshToken(RefreshTokenModel model)
        {
            var principal = tokenRepository.GetTokenPrincipal(model.JwtToken);
            var response = new LoginResponseDto();
            if(principal?.Identity?.Name is null)            
                return response;
            
            var identityUser = await userManager.FindByNameAsync(principal.Identity.Name);

            if(identityUser is null || identityUser.RefreshToken != model.RefreshToken 
                || identityUser.RefreshTokenExpiry < DateTime.Now)
                return response;

            response.JwtToken = tokenRepository.CreateToken(identityUser);
            response.RefreshToken = tokenRepository.CreateRefreshToken();

            identityUser.RefreshToken = response.RefreshToken;
            identityUser.RefreshTokenExpiry = DateTime.Now.AddHours(12);
            await userManager.UpdateAsync(identityUser);

            return response;
        }

        
    }
}
