namespace CustomerOrderAPI.DTOs
{
    // For status update endpoint
    public class UpdateOrderStatusDto
    {
        // Accepts strings like "Pending", "Confirmed", "Shipped"
        public string Status { get; set; } = string.Empty;
    }
}
