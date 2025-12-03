using CustomerOrderAPI.DTOs;
using CustomerOrderAPI.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<ActionResult<OrderDto>> CreateOrder(CreateOrderDto dto)
    {
        var created = await _orderService.CreateOrderAsync(dto);
        return CreatedAtAction(nameof(GetOrderById), new { id = created.OrderId }, created);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetOrderById(int id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        if (order == null) return NotFound();
        return order;
    }

    [HttpGet("customer/{customerId}")]
    public async Task<ActionResult<List<OrderDto>>> GetOrdersByCustomer(int customerId)
    {
        var orders = await _orderService.GetOrdersByCustomerAsync(customerId);
        return orders;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(int id, CreateOrderDto dto)
    {
        var success = await _orderService.UpdateOrderAsync(id, dto);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var success = await _orderService.DeleteOrderAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpPut("{orderId}/status")]
    public async Task<IActionResult> UpdateOrderStatus(int orderId, UpdateOrderStatusDto dto)
    {
        var result = await _orderService.UpdateOrderStatusAsync(orderId, dto);

        return result switch
        {
            "NOT_FOUND" => NotFound("Order not found"),
            "INVALID_STATUS" => BadRequest("Status must be Pending, Confirmed, or Shipped."),
            "ALREADY_IN_STATUS" => BadRequest("Order is already in the selected status"),
            "CONFIRM_REQUIRED" => BadRequest("Order must be confirmed before shipping"),
            "ALREADY_SHIPPED" => BadRequest("Order is already shipped and cannot be updated"),
            "SUCCESS" => Ok("Order status updated successfully"),
            _ => BadRequest("Unknown error")
        };
    }
}
