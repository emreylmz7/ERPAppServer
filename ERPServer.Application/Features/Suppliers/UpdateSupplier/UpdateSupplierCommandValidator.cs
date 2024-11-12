using FluentValidation;

namespace ERPServer.Application.Features.Suppliers.UpdateSupplier
{
    public sealed class UpdateSupplierCommandValidator : AbstractValidator<UpdateSupplierCommand>
    {
        public UpdateSupplierCommandValidator()
        {
            // Id: Required field for identifying the supplier to update
            RuleFor(s => s.Id)
                .NotEmpty().WithMessage("Supplier ID is required.");

            // CompanyName: Should not exceed 100 characters if provided
            RuleFor(s => s.CompanyName)
                .MaximumLength(100).WithMessage("Company name cannot exceed 100 characters.")
                .When(s => !string.IsNullOrEmpty(s.CompanyName));

            // ContactName: Should not exceed 100 characters if provided
            RuleFor(s => s.ContactName)
                .MaximumLength(100).WithMessage("Contact name cannot exceed 100 characters.")
                .When(s => !string.IsNullOrEmpty(s.ContactName));

            // ContactEmail: Should be a valid email and not exceed 150 characters if provided
            RuleFor(s => s.ContactEmail)
                .EmailAddress().WithMessage("Contact email must be a valid email address.")
                .MaximumLength(150).WithMessage("Contact email cannot exceed 150 characters.")
                .When(s => !string.IsNullOrEmpty(s.ContactEmail));

            // ContactPhone: Should not exceed 20 characters if provided
            RuleFor(s => s.ContactPhone)
                .MaximumLength(20).WithMessage("Contact phone cannot exceed 20 characters.")
                .When(s => !string.IsNullOrEmpty(s.ContactPhone));

            // Address: Should not exceed 500 characters if provided
            RuleFor(s => s.Address)
                .MaximumLength(500).WithMessage("Address cannot exceed 500 characters.")
                .When(s => !string.IsNullOrEmpty(s.Address));
        }
    }
}
