using CloupardTask.Service.DTOs.OrderDetails;
using System.ComponentModel.DataAnnotations;

namespace CloupardTask.Service.DTOs.Orders
{
    public class OrderCreateDto
    {
        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public ICollection<OrderDetailCreateDto> OrderDetails { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "TotalAmount must be greater than 0")]
        public decimal TotalAmount { get; set; }
    }
}
