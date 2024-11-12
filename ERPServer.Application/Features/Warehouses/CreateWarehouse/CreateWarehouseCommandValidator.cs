using FluentValidation;

namespace ERPServer.Application.Features.Warehouses.CreateWarehouse
{
    public sealed class CreateWarehouseCommandValidator : AbstractValidator<CreateWarehouseCommand>
    {
        public CreateWarehouseCommandValidator()
        {
            // WarehouseName: Should not be empty or null and have a maximum length
            RuleFor(w => w.WarehouseName)
                .NotEmpty().WithMessage("Warehouse name is required.")
                .MaximumLength(100).WithMessage("Warehouse name cannot exceed 100 characters.");

            // Location: Should not exceed a maximum length
            RuleFor(w => w.Location)
                .MaximumLength(500).WithMessage("Warehouse location cannot exceed 500 characters.");
        }
    }
}
