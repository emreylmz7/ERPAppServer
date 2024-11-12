using ERPServer.Domain.Enums;
using FluentValidation;

namespace ERPServer.Application.Features.StockMovements.UpdateStockMovement;

public sealed class UpdateStockMovementCommandValidator : AbstractValidator<UpdateStockMovementCommand>
{
    public UpdateStockMovementCommandValidator()
    {
        // Id: The stock movement ID must not be empty
        RuleFor(sm => sm.Id)
            .NotEmpty().WithMessage("Stock movement ID is required.");

        // ProductId: Should be a valid GUID if provided
        RuleFor(sm => sm.ProductId)
            .NotEmpty().WithMessage("Product ID is required.")
            .When(sm => sm.ProductId.HasValue);

        // MovementDate: Should be a valid date if provided
        RuleFor(sm => sm.MovementDate)
            .NotEmpty().WithMessage("Movement date is required.")
            .When(sm => sm.MovementDate.HasValue);

        // Quantity: Should be a valid integer if provided
        RuleFor(sm => sm.Quantity)
            .GreaterThanOrEqualTo(0).WithMessage("Quantity must be a non-negative number.")
            .When(sm => sm.Quantity.HasValue);

        // Type: Should be a valid enum value if provided
        RuleFor(sm => sm.Type)
                .Must(type => Enum.TryParse<MovementType>(type, out _))
                .WithMessage("Invalid movement type. Must be a valid MovementType value.");

        // OrderId: Should be a valid GUID if provided
        RuleFor(sm => sm.OrderId)
            .NotEmpty().WithMessage("Order ID is required.")
            .When(sm => sm.OrderId.HasValue);

        // SupplierId: Should be a valid GUID if provided
        RuleFor(sm => sm.SupplierId)
            .NotEmpty().WithMessage("Supplier ID is required.")
            .When(sm => sm.SupplierId.HasValue);

        // Description: Max length validation for description
        RuleFor(sm => sm.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.")
            .When(sm => !string.IsNullOrEmpty(sm.Description));

        // WarehouseId: Should be a valid GUID if provided
        RuleFor(sm => sm.WarehouseId)
            .NotEmpty().WithMessage("Warehouse ID is required.")
            .When(sm => sm.WarehouseId.HasValue);

        // Reason: Should be a valid enum value if provided
        RuleFor(sm => sm.Reason)
                .Must(reason => Enum.TryParse<StockMovementReason>(reason, out _))
                .WithMessage("Invalid reason type. Must be a valid StockMovementReason value.");
    }
}
