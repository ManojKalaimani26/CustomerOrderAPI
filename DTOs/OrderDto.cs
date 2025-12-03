namespace CustomerOrderAPI.DTOs
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string Product { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        // Exposed as string to API clients: "Pending", "Confirmed", "Shipped"
        public string Status { get; set; } = string.Empty;
    }
}
