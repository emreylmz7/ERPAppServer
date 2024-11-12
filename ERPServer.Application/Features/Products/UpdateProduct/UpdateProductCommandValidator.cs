using FluentValidation;

namespace ERPServer.Application.Features.Products.UpdateProduct;

public sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        // ProductId: Must not be empty
        RuleFor(p => p.Id)
            .NotEmpty().WithMessage("Product ID is required.");

        // SKU: Should not exceed 100 characters if provided
        RuleFor(p => p.SKU)
            .MaximumLength(100).WithMessage("SKU cannot exceed 100 characters.")
            .When(p => !string.IsNullOrEmpty(p.SKU));

        // Name: Should not exceed 100 characters if provided
        RuleFor(p => p.Name)
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.")
            .When(p => !string.IsNullOrEmpty(p.Name));

        // Description: Should not exceed 500 characters if provided
        RuleFor(p => p.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.")
            .When(p => !string.IsNullOrEmpty(p.Description));

        // Price: Should be a valid decimal if provided
        RuleFor(p => p.Price)
            .GreaterThan(0).WithMessage("Price must be a positive number.")
            .When(p => p.Price.HasValue);

        // Cost: Should be a valid decimal if provided
        RuleFor(p => p.Cost)
            .GreaterThan(0).WithMessage("Cost must be a positive number.")
            .When(p => p.Cost.HasValue);

        // StockQuantity: Should be a valid integer if provided
        RuleFor(p => p.StockQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Stock quantity must be a non-negative number.")
            .When(p => p.StockQuantity.HasValue);

        // CategoryId: Should be a valid GUID if provided
        RuleFor(p => p.CategoryId)
            .NotEmpty().WithMessage("Category ID is required.")
            .When(p => p.CategoryId.HasValue);

        // SupplierId: Should be a valid GUID if provided
        RuleFor(p => p.SupplierId)
            .NotEmpty().WithMessage("Supplier ID is required.")
            .When(p => p.SupplierId.HasValue);
    }
}
