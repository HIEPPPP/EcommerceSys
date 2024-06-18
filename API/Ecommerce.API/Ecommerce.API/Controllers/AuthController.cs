using Ecommerce.API.Models.Domain;
using Ecommerce.API.Models.DTO;
using Ecommerce.API.Repositories.Interface;
using Ecommerce.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthServices authServices;

        public AuthController(AuthServices authServices)
        {
            this.authServices = authServices;
        }

        //POST: /api/Auth/Register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await authServices.Register(registerRequestDto));
        }

        //POST: /api/Auth/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("User or password incorrect");
            }
            var loginResult = await authServices.Login(loginRequestDto);
            if(loginResult.JwtToken != null && loginResult.RefreshToken != null)
            {
                return Ok(loginResult);
            }
            return BadRequest("Username or password wrong!");
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenModel model)
        {
            var loginResult = await authServices.RefreshToken(model);
            if(loginResult != null)
            {
                return Ok(loginResult);
            }
            return Unauthorized();
        }
    }
}
