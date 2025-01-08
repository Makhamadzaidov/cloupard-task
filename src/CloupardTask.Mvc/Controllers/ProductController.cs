using CloupardTask.Api.Commons.Utils;
using CloupardTask.Api.DTO_s;
using CloupardTask.Domain.Models;
using CloupardTask.Service.Interfaces.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
        public async Task<IActionResult> UpdateProduct(string oldProductName, ProductUpdateDto product)
        {
            await _productService.UpdateAsync(oldProductName, product);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(string name)
        {
            await _productService.DeleteAsync(p => p.Name == name);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts(string name = null, [FromQuery] PaginationParams @params = null)
        {
            Expression<Func<Product, bool>> expression = name != null ? p => p.Name.Trim() == name : null;
            var products = await _productService.GetAllAsync(expression, @params);

            return View("Index", products);
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetails(int id)
        {
            var product = await _productService.GetAsync(p => p.Id == id);

            if (product == null)
            {
                return View("Error");
            }

            return View(product);
        }


        public async Task<ActionResult> LatestProducts()
		{
            var products = await _productService.GetAllAsync();

			return View(products.OrderByDescending(p => p.Price).Take(12).ToList());
		}
	}
}
