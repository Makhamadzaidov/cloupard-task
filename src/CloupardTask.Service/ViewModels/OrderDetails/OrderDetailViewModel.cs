namespace CloupardTask.Service.ViewModels.OrderDetails
{
    public class OrderDetailViewModel
    {
        // public int Id { get; set; }
        // public int ProductId { get; set; }
        public string ProductName { get; set; } // Add product name or details as needed
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;  // Calculated field for the total price of the order detail
    }
}
