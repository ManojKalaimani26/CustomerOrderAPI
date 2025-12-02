using CustomerOrderAPI.DTOs;
using FluentValidation;

public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
{
    public CreateOrderDtoValidator()
    {
        RuleFor(x => x.CustomerId).GreaterThan(0);
        RuleFor(x => x.Product).NotEmpty();
        RuleFor(x => x.Quantity).GreaterThan(0);
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.Status).NotEmpty().Must(s =>
            s == "Pending" || s == "Confirmed" || s == "Shipped")
            .WithMessage("Status must be Pending, Confirmed, or Shipped.");
    }
}
