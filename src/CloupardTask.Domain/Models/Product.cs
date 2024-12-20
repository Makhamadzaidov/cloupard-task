using CloupardTask.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace CloupardTask.Api.Models
{
    public class Product
    {
        [Key, Required]
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
