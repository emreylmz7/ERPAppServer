using FluentValidation;

namespace ERPServer.Application.Features.Suppliers.CreateSupplier
{
    public sealed class CreateSupplierCommandValidator : AbstractValidator<CreateSupplierCommand>
    {
        public CreateSupplierCommandValidator()
        {
            // CompanyName: Should not be empty or null and have a maximum length
            RuleFor(s => s.CompanyName)
                .NotEmpty().WithMessage("Company name is required.")
                .MaximumLength(100).WithMessage("Company name cannot exceed 100 characters.");

            // ContactName: Should not be empty or null and have a maximum length
            RuleFor(s => s.ContactName)
                .NotEmpty().WithMessage("Contact name is required.")
                .MaximumLength(100).WithMessage("Contact name cannot exceed 100 characters.");

            // ContactEmail: Should not be empty, valid email, and a maximum length
            RuleFor(s => s.ContactEmail)
                .NotEmpty().WithMessage("Contact email is required.")
                .EmailAddress().WithMessage("Contact email must be a valid email address.")
                .MaximumLength(150).WithMessage("Contact email cannot exceed 150 characters.");

            // ContactPhone: Should not be empty, valid phone number format, and a maximum length
            RuleFor(s => s.ContactPhone)
                .NotEmpty().WithMessage("Contact phone is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Contact phone must be a valid phone number.")
                .MaximumLength(20).WithMessage("Contact phone cannot exceed 20 characters.");

            // Address: Should not exceed a maximum length
            RuleFor(s => s.Address)
                .MaximumLength(500).WithMessage("Address cannot exceed 500 characters.");
        }
    }
}
