using ERPServer.Domain.Enums;
using FluentValidation;

namespace ERPServer.Application.Features.OrderItems.UpdateOrderItem;

public sealed class UpdateOrderItemCommandValidator : AbstractValidator<UpdateOrderItemCommand>
{
    public UpdateOrderItemCommandValidator()
    {
        // OrderItemId: Order item ID should not be empty
        RuleFor(o => o.Id)
            .NotEmpty().WithMessage("Order Item ID is required.");

        // ProductId: If provided, it must be a valid product ID
        RuleFor(o => o.ProductId)
            .NotEmpty().WithMessage("Product ID is required.")
            .When(o => o.ProductId.HasValue);

        // Quantity: If provided, quantity must be greater than 0
        RuleFor(o => o.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.")
            .When(o => o.Quantity.HasValue);

        // UnitPrice: If provided, unit price must be greater than or equal to 0
        RuleFor(o => o.UnitPrice)
            .GreaterThanOrEqualTo(0).WithMessage("Unit price must be greater than or equal to zero.")
            .When(o => o.UnitPrice.HasValue);

        // Discount: If provided, discount must be greater than or equal to 0
        RuleFor(o => o.Discount)
            .GreaterThanOrEqualTo(0).WithMessage("Discount must be greater than or equal to zero.")
            .When(o => o.Discount.HasValue);
    }
}
