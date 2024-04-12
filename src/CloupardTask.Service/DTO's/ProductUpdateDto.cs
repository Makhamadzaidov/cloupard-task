using System.ComponentModel.DataAnnotations;

namespace CloupardTask.Api.DTO_s
{
	public class ProductUpdateDto
	{
		[Required]
		public Guid Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
	}
}
