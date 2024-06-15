using Ecommerce.API.Models.DTO;
using Ecommerce.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly UserServices userServices;

        public UserController(UserServices userServices)
        {
            this.userServices = userServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            return Ok(await userServices.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            return Ok(await userServices.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUpdateUserRequestDto dto)
        {
            return Ok(await userServices.CreateAsync(dto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            return Ok(await userServices.DeleteAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, CreateUpdateUserRequestDto dto)
        {
            return Ok(await userServices.UpdateAsync(id, dto)); 
        }
    }
}
