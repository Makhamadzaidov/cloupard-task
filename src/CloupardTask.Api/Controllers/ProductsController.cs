using CloupardTask.Api.Commons.Utils;
using CloupardTask.Api.DTO_s;
using CloupardTask.Api.Models;
using CloupardTask.Service.Interfaces.Products;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace CloupardTask.Api.Controllers
{
    [Route("api/Products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateAsync([FromForm] ProductCreateDto product)
        {
            return Ok(await _productService.CreateAsync(product));
        }

        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteAsync([FromForm, Required] string Name)
        {
            return Ok(await _productService.DeleteAsync(p => p.Name == Name));
        }

        [HttpPatch("UpdateProduct")]
        public async Task<IActionResult> UpdateAsync([FromQuery] string oldProductName, [FromForm] ProductUpdateDto product)
        {
            return Ok(await _productService.UpdateAsync(oldProductName, product));
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllAsync(string? name = null, [FromQuery] PaginationParams @params = null)
        {
            Expression<Func<Product, bool>> expression = name != null ? p => p.Name.Trim() == name : null;
            return Ok(await _productService.GetAllAsync(expression, @params));
        }

        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetAsync([FromQuery] string name)
        {
            return Ok(await _productService.GetAsync(p => p.Name == name));
        }
    }
}
