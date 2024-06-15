using Ecommerce.API.Models.DTO;
using Ecommerce.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class DiscountController : ControllerBase
    {
        private readonly DiscountServices discountServices;

        public DiscountController(DiscountServices discountServices)
        {
            this.discountServices = discountServices;
        }
        [HttpPost]
        public async Task<IActionResult> CreateDiscount(CreateUpdateDiscountRequestDto dto)
        {
            return Ok(await discountServices.CreateDiscountAsync(dto));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDiscount()
        {
            return Ok(await discountServices.GetAllDiscountAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiscount(int id)
        {
            return Ok(await discountServices.GetDiscountAsync(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscount(int id)
        {
            return Ok(await discountServices.DeleteDiscountAsync(id));  
        }

        [HttpPut("{id}")] 
        public async Task<IActionResult> UpdateDiscount(int id, CreateUpdateDiscountRequestDto dto)
        {
            return Ok(await discountServices.UpdateDiscountAsync(id, dto));
        }
    }
}
