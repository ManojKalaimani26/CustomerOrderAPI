using CustomerOrderAPI.Data;
using CustomerOrderAPI.Repositories;
using CustomerOrderAPI.Services;
using CustomerOrderAPI.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------
// Serilog
// ---------------------------
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

// ---------------------------
// Add services to container
// ---------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ---------------------------
// Database Connection
// ---------------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ---------------------------
// AutoMapper
// ---------------------------
// MappingProfile must exist in your project
builder.Services.AddAutoMapper(typeof(MappingProfile));

// ---------------------------
// FluentValidation
// ---------------------------
builder.Services.AddFluentValidationAutoValidation();

// Register ALL validators in the assembly
builder.Services.AddValidatorsFromAssemblyContaining<CreateOrderDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateCustomerDtoValidator>();

// ---------------------------
// Dependency Injection
// ---------------------------
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

// If you have CustomerService or CustomerRepository, register them too.
// builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
// builder.Services.AddScoped<ICustomerService, CustomerService>();

// ---------------------------
// Build & Run
// ---------------------------
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
