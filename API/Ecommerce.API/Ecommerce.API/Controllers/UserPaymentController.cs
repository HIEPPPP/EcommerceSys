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
    public class UserPaymentController : ControllerBase
    {
        private readonly UserPaymentServieces userPaymentServieces;

        public UserPaymentController(UserPaymentServieces userPaymentServieces)
        {
            this.userPaymentServieces = userPaymentServieces;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUPayment()
        {
            return Ok(await userPaymentServieces.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUPayment(int id)
        {
            return Ok(await userPaymentServieces.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUPayment(CreateUPaymentRequestDto dto)
        {
            return Ok(await userPaymentServieces.CreateAsync(dto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUPayment(int id)
        {
            return Ok(await userPaymentServieces.DeleteAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUPayment(int id, UpdateUPaymentRequestDto dto)
        {
            return Ok(await userPaymentServieces.UpdateAsync(id, dto));
        }
    }
}
