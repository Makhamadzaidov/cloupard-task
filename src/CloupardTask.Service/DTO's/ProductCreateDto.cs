using System.ComponentModel.DataAnnotations;

namespace CloupardTask.Api.DTO_s
{
	public class ProductCreateDto
	{
		[Required]
		public string Name { get; set; } = null!;
		public string? Description { get; set; }
	}
}
