using AutoMapper;
using Ecommerce.API.Data;
using Ecommerce.API.Models.Domain;
using Ecommerce.API.Models.DTO;
using Ecommerce.API.Repositories.Implementation;
using Ecommerce.API.Repositories.Interface;
using Ecommerce.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ProductController : ControllerBase
    {
        private readonly ProductServices productService;

        public ProductController( ProductServices productService )
        {
            this.productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductRequestDto createProductRequestDto)
        {
            return Ok(await productService.CreateAsync(createProductRequestDto));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct([FromQuery] string? filterOn = null, [FromQuery] string? filterQuery = null,
                                                        [FromQuery] string? sortBy = null, [FromQuery] bool? isAscending = null,
                                                        [FromQuery] int pageNumbers = 1, [FromQuery] int pageSize = 100)
        {
            return Ok(await productService.GetProductsAsync(filterOn, filterQuery, sortBy, isAscending, pageNumbers, pageSize));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdProduct(int id)
        {
            return Ok(await productService.GetProductAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductRequestDto updateProductRequestDto)
        {
            return Ok(await productService.UpdateProductAsync(id, updateProductRequestDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return Ok(await productService.DeleteProductAsync(id));
        }

    }
}
