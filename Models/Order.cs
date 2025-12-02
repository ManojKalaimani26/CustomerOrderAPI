namespace CustomerOrderAPI.Models
{
    public enum OrderStatus
    {
        Pending,
        Confirmed,
        Shipped
    }

    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public Customer Customer { get; set; }
    }
}