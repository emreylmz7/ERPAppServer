using FluentValidation;

namespace ERPServer.Application.Features.Products.CreateProduct;

public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        // Name: Must not be empty and should have a reasonable max length
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(200).WithMessage("Product name cannot exceed 200 characters.");

        // Description: Max length validation
        RuleFor(p => p.Description)
            .MaximumLength(500).WithMessage("Product description cannot exceed 500 characters.");

        // Price: Must be greater than 0
        RuleFor(p => p.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");

        // Cost: Must be greater than or equal to zero
        RuleFor(p => p.Cost)
            .GreaterThanOrEqualTo(0).WithMessage("Cost must be greater than or equal to zero.");

        // StockQuantity: Must be a non-negative integer
        RuleFor(p => p.StockQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Stock quantity cannot be negative.");

        // CategoryId: Must not be empty and must refer to a valid category
        RuleFor(p => p.CategoryId)
            .NotEmpty().WithMessage("Category ID is required.");

        // SupplierId: Must not be empty and must refer to a valid supplier
        RuleFor(p => p.SupplierId)
            .NotEmpty().WithMessage("Supplier ID is required.");
    }
}
