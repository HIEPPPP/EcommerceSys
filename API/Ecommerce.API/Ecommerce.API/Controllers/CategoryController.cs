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
    public class CategoryController : ControllerBase
    {
        private readonly CategoryServices categoryServices;

        public CategoryController(CategoryServices categoryServices)
        {
            this.categoryServices = categoryServices;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto createCategoryRequestDto)
        {
            return Ok(await categoryServices.CreateCategory(createCategoryRequestDto));
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            throw new Exception("This is custom exception");

            return Ok(await categoryServices.GetCategories());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            return Ok(await categoryServices.GetCategory(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            return Ok(await categoryServices.DeleteCategoryAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryRequestDto updateCategoryRequestDto)
        {
            return Ok(await categoryServices.UpdateCategory(id, updateCategoryRequestDto));
        }
    }
}
