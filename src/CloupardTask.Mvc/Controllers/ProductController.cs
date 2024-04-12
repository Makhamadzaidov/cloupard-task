using CloupardTask.Api.Commons.Utils;
using CloupardTask.Api.DTO_s;
using CloupardTask.Api.Interfaces.Services;
using CloupardTask.Api.Models;
using CloupardTask.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace CloupardTask.Mvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();

            return View(products);
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductCreateDto product)
        {
            await _productService.CreateAsync(product);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ProductUpdateDto product)
        {
            await _productService.UpdateAsync(product);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            await _productService.DeleteAsync(p => p.Id == Id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts(string name = null, [FromQuery] PaginationParams @params = null)
        {
            IEnumerable<Product> products;

            if (!string.IsNullOrEmpty(name))
            {
                products = await _productService.GetAllAsync(p => p.Name.Trim().Contains(name), @params);
            }
            else
            {
                products = await _productService.GetAllAsync(null, @params);
            }

            return View("Index", products);
        }
    }
}
