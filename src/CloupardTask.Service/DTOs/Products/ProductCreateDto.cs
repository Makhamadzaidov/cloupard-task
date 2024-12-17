using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CloupardTask.Api.DTO_s
{
	public class ProductCreateDto
	{
        [Required(ErrorMessage = "Name is required field")]
        [MaxLength(35), MinLength(2)]
        public string Name { get; set; } = null!;
		public string? Description { get; set; }

        [Required(ErrorMessage = "Price is required field")]
        public decimal Price { get; set; }
        public IFormFile? Image { get; set; }

        [Required(ErrorMessage = "Category Name is required field")]
        public string CategoryName { get; set; }
    }
}
