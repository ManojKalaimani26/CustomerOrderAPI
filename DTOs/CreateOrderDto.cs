namespace CustomerOrderAPI.DTOs
{
    // For creating or updating an order's basic details (not status)
    public class CreateOrderDto
    {
        public int CustomerId { get; set; }
        public string Product { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        // Status is NOT set here; new orders start as Pending.
    }
}
