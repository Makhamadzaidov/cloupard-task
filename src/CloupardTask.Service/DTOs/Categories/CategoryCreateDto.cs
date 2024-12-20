using System.ComponentModel.DataAnnotations;

namespace CloupardTask.Service.DTOs.Categories
{
    public class CategoryCreateDto
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
