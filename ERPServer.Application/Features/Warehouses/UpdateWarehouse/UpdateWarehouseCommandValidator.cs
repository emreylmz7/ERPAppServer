using FluentValidation;

namespace ERPServer.Application.Features.Warehouses.UpdateWarehouse
{
    public sealed class UpdateWarehouseCommandValidator : AbstractValidator<UpdateWarehouseCommand>
    {
        public UpdateWarehouseCommandValidator()
        {
            // Id: Required field for identifying the warehouse to update
            RuleFor(w => w.Id)
                .NotEmpty().WithMessage("Warehouse ID is required.");

            // WarehouseName: Should not exceed 100 characters if provided
            RuleFor(w => w.WarehouseName)
                .MaximumLength(100).WithMessage("Warehouse name cannot exceed 100 characters.")
                .When(w => !string.IsNullOrEmpty(w.WarehouseName));

            // Location: Should not exceed 500 characters if provided
            RuleFor(w => w.Location)
                .MaximumLength(500).WithMessage("Location cannot exceed 500 characters.")
                .When(w => !string.IsNullOrEmpty(w.Location));
        }
    }
}
