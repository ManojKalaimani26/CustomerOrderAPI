using CustomerOrderAPI.Models;

namespace CustomerOrderAPI.Repositories
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(int orderId);
        Task<List<Order>> GetByCustomerAsync(int customerId);
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(Order order);
    }
}
