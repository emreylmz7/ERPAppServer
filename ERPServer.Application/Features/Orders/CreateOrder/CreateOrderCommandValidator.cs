using ERPServer.Domain.Enums;
using FluentValidation;

namespace ERPServer.Application.Features.Orders.CreateOrder;

public sealed class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        // CustomerId: Must not be empty
        RuleFor(o => o.CustomerId)
            .NotEmpty().WithMessage("Customer ID is required.");

        // ShippingAddressId: Must not be empty
        RuleFor(o => o.ShippingAddressId)
            .NotEmpty().WithMessage("Shipping Address ID is required.");

        // Status: Must be a valid OrderStatus value
        //RuleFor(o => o.Status)
        //    .Must(status => Enum.TryParse<OrderStatus>(status, out _))
        //    .WithMessage("Invalid order status. Must be a valid OrderStatus value.");

        // TaxAmount: Must be zero or positive
        RuleFor(o => o.TaxRate)
            .GreaterThanOrEqualTo(0).WithMessage("Tax amount must be zero or greater.");

        // ShippingFee: Must be zero or positive
        RuleFor(o => o.ShippingFee)
            .GreaterThanOrEqualTo(0).WithMessage("Shipping fee must be zero or greater.");

        //RuleFor(o => o.PaymentMethod)
        //    .Must(method => Enum.TryParse<PaymentMethod>(method, out _))
        //    .WithMessage("Invalid payment method. Must be a valid PaymentMethod value.");
    }
}
