using Microsoft.AspNetCore.Http;

namespace CloupardTask.Api.DTO_s
{
    public class ProductUpdateDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? CategoryName { get; set; }
        public IFormFile? Image { get; set; }
    }
}
