using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomerOrderAPI.Data;
using CustomerOrderAPI.DTOs;
using CustomerOrderAPI.Models;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public CustomerController(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customers = await _db.Customers.ToListAsync();
        var result = _mapper.Map<List<CustomerDto>>(customers);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var customer = await _db.Customers.FindAsync(id);
        if (customer == null) return NotFound();
        return Ok(_mapper.Map<CustomerDto>(customer));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCustomerDto dto)
    {
        var customer = _mapper.Map<Customer>(dto);
        _db.Customers.Add(customer);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = customer.CustomerId }, _mapper.Map<CustomerDto>(customer));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreateCustomerDto dto)
    {
        var customer = await _db.Customers.FindAsync(id);
        if (customer == null) return NotFound();

        _mapper.Map(dto, customer);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var customer = await _db.Customers.FindAsync(id);
        if (customer == null) return NotFound();

        _db.Customers.Remove(customer);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}