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
    public class UserAddressController : ControllerBase
    {
        private readonly UAddressServices uAddressServices;

        public UserAddressController(UAddressServices uAddressServices)
        {
            this.uAddressServices = uAddressServices;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUAddress(CreateUpdateUAddressRequestDto dto)
        {
            return Ok(await uAddressServices.CreateAsync(dto));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUAdress()
        {
            return Ok(await uAddressServices.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUAddress(int id)
        {
            return Ok(await uAddressServices.GetAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUAddress(int id, CreateUpdateUAddressRequestDto dto)
        {
            return Ok(await uAddressServices.UpdateAsync(id, dto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUAddress(int id)
        {
            return Ok(await uAddressServices.DeleteAsync(id));  
        }
    }
}
