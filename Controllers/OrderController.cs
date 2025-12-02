using AutoMapper;
using CustomerOrderAPI.Data;
using CustomerOrderAPI.DTOs;
using CustomerOrderAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public OrderController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<OrderDto>> CreateOrder(CreateOrderDto dto)
    {
        var order = _mapper.Map<Order>(dto);
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        var result = _mapper.Map<OrderDto>(order);
        return CreatedAtAction(nameof(GetOrderById), new { id = order.OrderId }, result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetOrderById(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null) return NotFound();
        return _mapper.Map<OrderDto>(order);
    }

    [HttpGet("customer/{customerId}")]
    public async Task<ActionResult<List<OrderDto>>> GetOrdersByCustomer(int customerId)
    {
        var orders = await _context.Orders
            .Where(o => o.CustomerId == customerId)
            .ToListAsync();
        return _mapper.Map<List<OrderDto>>(orders);
    }

    //[HttpPut("{id}")]
    //public async Task<IActionResult> UpdateOrder(int id, CreateOrderDto dto)
    //{
    //    var order = await _context.Orders.FindAsync(id);
    //    if (order == null) return NotFound();

    //    _mapper.Map(dto, order);
    //    await _context.SaveChangesAsync();
    //    return NoContent();
    //}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(int id, CreateOrderDto dto)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null) return NotFound();

        // Status validation
        if (dto.Status == "Shipped" && order.Status != OrderStatus.Confirmed)
            return BadRequest("Cannot ship an order before it is confirmed.");

        _mapper.Map(dto, order);
        await _context.SaveChangesAsync();
        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null) return NotFound();

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}