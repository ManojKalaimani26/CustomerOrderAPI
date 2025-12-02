namespace CustomerOrderAPI.DTOs
{
    public class CreateOrderDto
    {
        public int CustomerId { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
    }
}
