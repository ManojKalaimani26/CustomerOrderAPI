using AutoMapper;
using CustomerOrderAPI.Models;
using CustomerOrderAPI.DTOs;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Customer, CustomerDto>().ReverseMap();
        CreateMap<CreateCustomerDto, Customer>();
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<CreateOrderDto, Order>();
    }
}