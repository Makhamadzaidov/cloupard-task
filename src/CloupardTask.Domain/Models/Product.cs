using System.ComponentModel.DataAnnotations;

namespace CloupardTask.Api.Models
{
	public class Product
	{
		[Key, Required]
		public Guid Id { get; set; }

		[Required, MaxLength(255)]
		public string Name { get; set; } = null!;
		public string? Description { get; set; }
	}
}
