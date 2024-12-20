using CloupardTask.Service.DTOs.OrderDetails;
using System.ComponentModel.DataAnnotations;

namespace CloupardTask.Service.DTOs.Orders
{
    public class OrderUpdateDto
    {
        public DateTime? OrderDate { get; set; }

        public int? CustomerId { get; set; }

        public ICollection<OrderDetailUpdateDto> OrderDetails { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "TotalAmount must be greater than 0")]
        public decimal? TotalAmount { get; set; }
    }
}
