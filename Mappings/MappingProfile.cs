using AutoMapper;
using CustomerOrderAPI.Models;
using CustomerOrderAPI.DTOs;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // CUSTOMER mappings
        CreateMap<Customer, CustomerDto>().ReverseMap();
        CreateMap<CreateCustomerDto, Customer>();

        // ORDER mappings
        CreateMap<CreateOrderDto, Order>();

        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        // DO NOT ReverseMap() here because OrderDto.Status is string
        // and Order.Status is enum
    }
}
