using CustomerOrderAPI.DTOs;
using CustomerOrderAPI.Models;
using CustomerOrderAPI.Repositories;

namespace CustomerOrderAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        private static OrderDto ToDto(Order order) =>
            new OrderDto
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                Product = order.Product,
                Quantity = order.Quantity,
                Price = order.Price,
                Status = order.Status.ToString()
            };

        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto dto)
        {
            var order = new Order
            {
                CustomerId = dto.CustomerId,
                Product = dto.Product,
                Quantity = dto.Quantity,
                Price = dto.Price,
                Status = OrderStatus.Pending // new orders start as Pending
            };

            await _orderRepository.AddAsync(order);
            return ToDto(order);
        }

        public async Task<OrderDto?> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            return order == null ? null : ToDto(order);
        }

        public async Task<List<OrderDto>> GetOrdersByCustomerAsync(int customerId)
        {
            var orders = await _orderRepository.GetByCustomerAsync(customerId);
            return orders.Select(ToDto).ToList();
        }

        public async Task<bool> UpdateOrderAsync(int id, CreateOrderDto dto)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                return false;

            // Update basic fields only; status handled separately
            order.Product = dto.Product;
            order.Quantity = dto.Quantity;
            order.Price = dto.Price;

            await _orderRepository.UpdateAsync(order);
            return true;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                return false;

            await _orderRepository.DeleteAsync(order);
            return true;
        }

        public async Task<string> UpdateOrderStatusAsync(int orderId, UpdateOrderStatusDto dto)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                return "NOT_FOUND";

            if (!Enum.TryParse<OrderStatus>(dto.Status, true, out var newStatus))
                return "INVALID_STATUS";

            if (order.Status == newStatus)
                return "ALREADY_IN_STATUS";

            // Business rules
            if (order.Status == OrderStatus.Pending && newStatus == OrderStatus.Shipped)
                return "CONFIRM_REQUIRED";

            if (order.Status == OrderStatus.Shipped)
                return "ALREADY_SHIPPED";

            order.Status = newStatus;
            await _orderRepository.UpdateAsync(order);

            return "SUCCESS";
        }
    }
}
