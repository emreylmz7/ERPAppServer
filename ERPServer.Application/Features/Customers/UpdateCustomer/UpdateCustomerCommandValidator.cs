using FluentValidation;

namespace ERPServer.Application.Features.Customers.UpdateCustomer;

public sealed class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        // CustomerId: Must not be empty
        RuleFor(p => p.Id)
            .NotEmpty().WithMessage("Customer ID is required.");

        // Only validate fields that are provided (nullable fields)

        // FirstName: Should not exceed 100 characters if provided
        RuleFor(p => p.FirstName)
            .MaximumLength(100).WithMessage("First name cannot exceed 100 characters.");

        // LastName: Should not exceed 100 characters if provided
        RuleFor(p => p.LastName)
            .MaximumLength(100).WithMessage("Last name cannot exceed 100 characters.");

        // TaxNumber: Should be a 10 or 11 digit number if provided
        RuleFor(p => p.TaxNumber)
            .Matches(@"^\d{10,11}$").WithMessage("Tax number must be a 10 or 11 digit number.")
            .When(p => !string.IsNullOrEmpty(p.TaxNumber));

        // TaxOffice: Should not exceed 200 characters if provided
        RuleFor(p => p.TaxOffice)
            .MaximumLength(200).WithMessage("Tax office cannot exceed 200 characters.")
            .When(p => !string.IsNullOrEmpty(p.TaxOffice));

        // City: Should not exceed 100 characters if provided
        RuleFor(p => p.City)
            .MaximumLength(100).WithMessage("City name cannot exceed 100 characters.")
            .When(p => !string.IsNullOrEmpty(p.City));

        // District: Should not exceed 100 characters if provided
        RuleFor(p => p.District)
            .MaximumLength(100).WithMessage("District name cannot exceed 100 characters.")
            .When(p => !string.IsNullOrEmpty(p.District));

        // Address: Should not exceed 500 characters if provided
        RuleFor(p => p.Address)
            .MaximumLength(500).WithMessage("Address cannot exceed 500 characters.")
            .When(p => !string.IsNullOrEmpty(p.Address));

        // Email: Should be a valid email format if provided
        RuleFor(p => p.Email)
            .EmailAddress().WithMessage("Invalid email address format.")
            .MaximumLength(255).WithMessage("Email address cannot exceed 255 characters.")
            .When(p => !string.IsNullOrEmpty(p.Email));

        // PhoneNumber: Should be a valid phone number if provided
        RuleFor(p => p.PhoneNumber)
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid phone number format.")
            .When(p => !string.IsNullOrEmpty(p.PhoneNumber));

        // CustomerType: Should be either 'Individual' or 'Corporate' if provided
        RuleFor(p => p.CustomerType)
            .Must(type => type == "Individual" || type == "Corporate")
            .WithMessage("Customer type must be either 'Individual' or 'Corporate'.")
            .When(p => !string.IsNullOrEmpty(p.CustomerType));

        // Status: Should be either 'Active' or 'Inactive' if provided
        RuleFor(p => p.Status)
            .Must(status => status == "Active" || status == "Inactive")
            .WithMessage("Status must be either 'Active' or 'Inactive'.")
            .When(p => !string.IsNullOrEmpty(p.Status));

        // CustomerGroup: Should be either 'VIP' or 'Normal' if provided
        RuleFor(p => p.CustomerGroup)
            .Must(group => group == "VIP" || group == "Normal")
            .WithMessage("Customer group must be either 'VIP' or 'Normal'.")
            .When(p => !string.IsNullOrEmpty(p.CustomerGroup));
    }
}
