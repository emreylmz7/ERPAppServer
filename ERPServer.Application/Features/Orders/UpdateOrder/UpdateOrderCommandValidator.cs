using ERPServer.Domain.Enums;
using FluentValidation;

namespace ERPServer.Application.Features.Orders.UpdateOrder;

public sealed class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        // OrderId: Sipariş ID'si boş olamaz
        RuleFor(o => o.Id)
            .NotEmpty().WithMessage("Order ID is required.");

        // CustomerId: Eğer sağlanmışsa, geçerli bir ID olmalı
        RuleFor(o => o.CustomerId)
            .NotEmpty().WithMessage("Customer ID is required.")
            .When(o => o.CustomerId.HasValue);

        // OrderDate: Eğer sağlanmışsa, geçerli bir tarih olmalı
        RuleFor(o => o.OrderDate)
            .NotEmpty().WithMessage("Order date is required.")
            .When(o => o.OrderDate.HasValue);

        // ShippingFee: Eğer sağlanmışsa, sıfırdan büyük olmalı
        RuleFor(o => o.ShippingFee)
            .GreaterThanOrEqualTo(0).WithMessage("Shipping fee must be greater than or equal to zero.")
            .When(o => o.ShippingFee.HasValue);

        // Status: Geçerli bir OrderStatus enum değeri olmalı
        //RuleFor(o => o.Status)
        //    .Must(status => Enum.TryParse<OrderStatus>(status, out _))
        //    .WithMessage("Invalid order status. Must be a valid OrderStatus value.");

        // ShippingAddressId: Eğer sağlanmışsa, geçerli bir ID olmalı
        RuleFor(o => o.ShippingAddressId)
            .NotEmpty().WithMessage("Shipping address ID is required.")
            .When(o => o.ShippingAddressId.HasValue);

        // PaymentMethod: Eğer sağlanmışsa, geçerli bir PaymentMethod enum değeri olmalı
        //RuleFor(o => o.PaymentMethod)
        //    .Must(method => Enum.TryParse<PaymentMethod>(method, out _))
        //    .WithMessage("Invalid payment method. Must be a valid PaymentMethod value.");

        // ShippingDate: Eğer sağlanmışsa, geçerli bir tarih olmalı
        RuleFor(o => o.ShippingDate)
            .NotEmpty().WithMessage("Shipping date is required.")
            .When(o => o.ShippingDate.HasValue);

        // DeliveryDate: Eğer sağlanmışsa, geçerli bir tarih olmalı
        RuleFor(o => o.DeliveryDate)
            .NotEmpty().WithMessage("Delivery date is required.")
            .When(o => o.DeliveryDate.HasValue);

    }
}
