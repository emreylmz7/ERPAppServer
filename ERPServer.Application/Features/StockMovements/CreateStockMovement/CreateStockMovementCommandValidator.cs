using ERPServer.Domain.Enums;
using FluentValidation;

namespace ERPServer.Application.Features.StockMovements.CreateStockMovement;

public sealed class CreateStockMovementCommandValidator : AbstractValidator<CreateStockMovementCommand>
{
    public CreateStockMovementCommandValidator()
    {
        // ProductId: Must not be empty and must exist in the database
        RuleFor(p => p.ProductId)
            .NotEmpty().WithMessage("Product ID is required.");

        // Quantity: Must be a positive integer
        RuleFor(p => p.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

        RuleFor(sm => sm.Type)
                .Must(type => Enum.TryParse<MovementType>(type, out _))
                .WithMessage("Invalid movement type. Must be a valid MovementType value.");

        RuleFor(sm => sm.Reason)
                .Must(reason => Enum.TryParse<StockMovementReason>(reason, out _))
                .WithMessage("Invalid reason type. Must be a valid StockMovementReason value.");

        // Type: Must be either Inbound or Outbound
        //RuleFor(p => p.Type)
        //    .IsInEnum().WithMessage("Invalid movement type. It must be either Inbound or Outbound.");

        // WarehouseId: Must not be empty and must exist in the database
        //RuleFor(p => p.WarehouseId)
        //    .NotEmpty().WithMessage("Warehouse ID is required.");

        //// Reason: Must be a valid enum value (Sale, Purchase, Adjustment, Transfer)
        //RuleFor(p => p.Reason)
        //    .IsInEnum().WithMessage("Invalid reason. Must be one of Sale, Purchase, Adjustment, or Transfer.");
    }
}
