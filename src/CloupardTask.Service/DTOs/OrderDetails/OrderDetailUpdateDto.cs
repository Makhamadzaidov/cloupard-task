using System.ComponentModel.DataAnnotations;

namespace CloupardTask.Service.DTOs.OrderDetails
{
    public class OrderDetailUpdateDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int? Quantity { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "UnitPrice must be greater than 0")]
        public decimal? UnitPrice { get; set; }
    }
}
