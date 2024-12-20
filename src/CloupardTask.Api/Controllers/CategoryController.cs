using CloupardTask.Api.Commons.Utils;
using CloupardTask.Service.DTOs.Categories;
using CloupardTask.Service.Interfaces.Categories;
using Microsoft.AspNetCore.Mvc;

namespace CloupardTask.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // POST: api/Category
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _categoryService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetCategory), new { name = result.Name }, result);
        }

        // GET: api/Category/{name}
        [HttpGet("{name}")]
        public async Task<IActionResult> GetCategory(string name)
        {
            var category = await _categoryService.GetAsync(c => c.Name == name);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        // GET: api/Category
        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] PaginationParams paginationParams)
        {
            var categories = await _categoryService.GetAllAsync(null, paginationParams);
            return Ok(categories);
        }

        // PUT: api/Category/{oldCategoryName}
        [HttpPut("{oldCategoryName}")]
        public async Task<IActionResult> UpdateCategory(string oldCategoryName, [FromBody] CategoryCreateDto updatedCategory)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _categoryService.UpdateAsync(oldCategoryName, updatedCategory);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // DELETE: api/Category/{name}
        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteCategory(string name)
        {
            var isDeleted = await _categoryService.DeleteAsync(name);
            if (!isDeleted)
                return NotFound();

            return NoContent();
        }
    }
}
