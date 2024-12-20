using CloupardTask.Service.ViewModels.OrderDetails;

namespace CloupardTask.Service.ViewModels.Orders
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        // public int CustomerId { get; set; }
        public string CustomerName { get; set; }  // Add customer name or details as needed
        public ICollection<OrderDetailViewModel> OrderDetails { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
