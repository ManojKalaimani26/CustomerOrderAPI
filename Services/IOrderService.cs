using CustomerOrderAPI.DTOs;

namespace CustomerOrderAPI.Services
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(CreateOrderDto dto);
        Task<OrderDto?> GetOrderByIdAsync(int id);
        Task<List<OrderDto>> GetOrdersByCustomerAsync(int customerId);
        Task<bool> UpdateOrderAsync(int id, CreateOrderDto dto);
        Task<bool> DeleteOrderAsync(int id);
        Task<string> UpdateOrderStatusAsync(int orderId, UpdateOrderStatusDto dto);
    }
}
