namespace CustomerOrderAPI.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }

        public string Product { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Confirmed, Shipped

        public Customer? Customer { get; set; }
    }
}