using FluentValidation;

namespace ERPServer.Application.Features.OrderItems.CreateOrderItem;

public sealed class CreateOrderItemCommandValidator : AbstractValidator<CreateOrderItemCommand>
{
    public CreateOrderItemCommandValidator()
    {

        // ProductId: Must not be empty
        RuleFor(o => o.ProductId)
            .NotEmpty().WithMessage("Product ID is required.");

        // Quantity: Must be greater than 0
        RuleFor(o => o.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

        // Discount: Must be zero or positive and less than or equal to the UnitPrice
        RuleFor(o => o.Discount)
            .GreaterThanOrEqualTo(0).WithMessage("Discount must be zero or greater.");
            
    }
}
