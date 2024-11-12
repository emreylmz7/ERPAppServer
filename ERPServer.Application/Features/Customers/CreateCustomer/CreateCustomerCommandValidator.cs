using FluentValidation;

namespace ERPServer.Application.Features.Customers.CreateCustomer;

public sealed class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(p => p.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(100).WithMessage("First name cannot exceed 100 characters.");

        // LastName: Should not be empty or null
        RuleFor(p => p.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(100).WithMessage("Last name cannot exceed 100 characters.");

        // TaxNumber: Should be between 10 and 11 characters, numeric
        RuleFor(p => p.TaxNumber)
            .NotEmpty().WithMessage("Tax number is required.")
            .Matches(@"^\d{10,11}$").WithMessage("Tax number must be a 10 or 11 digit number.");

        // TaxOffice: Should not be empty
        RuleFor(p => p.TaxOffice)
            .NotEmpty().WithMessage("Tax office is required.")
            .MaximumLength(200).WithMessage("Tax office cannot exceed 200 characters.");

        // City: Should not be empty
        RuleFor(p => p.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(100).WithMessage("City name cannot exceed 100 characters.");

        // District: Should not be empty
        RuleFor(p => p.District)
            .NotEmpty().WithMessage("District is required.")
            .MaximumLength(100).WithMessage("District name cannot exceed 100 characters.");

        // Address: Should not be empty
        RuleFor(p => p.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(500).WithMessage("Address cannot exceed 500 characters.");

        // Email: Should be a valid email format
        RuleFor(p => p.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email address format.")
            .MaximumLength(255).WithMessage("Email address cannot exceed 255 characters.");

        // PhoneNumber: Should be a valid phone number (e.g., matching a pattern)
        RuleFor(p => p.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid phone number format.");

        // CustomerType: Should not be empty and check for known values (example: Individual, Corporate)
        RuleFor(p => p.CustomerType)
            .NotEmpty().WithMessage("Customer type is required.")
            .Must(type => type == "Individual" || type == "Corporate")
            .WithMessage("Customer type must be either 'Individual' or 'Corporate'.");

        // Status: Should not be empty and check for known values (e.g., Active, Inactive)
        RuleFor(p => p.Status)
            .NotEmpty().WithMessage("Status is required.")
            .Must(status => status == "Active" || status == "Inactive")
            .WithMessage("Status must be either 'Active' or 'Inactive'.");

        // CustomerGroup: Should not be empty and check for known values (e.g., VIP, Normal)
        RuleFor(p => p.CustomerGroup)
            .NotEmpty().WithMessage("Customer group is required.")
            .Must(group => group == "VIP" || group == "Normal")
            .WithMessage("Customer group must be either 'VIP' or 'Normal'.");
    }
}



