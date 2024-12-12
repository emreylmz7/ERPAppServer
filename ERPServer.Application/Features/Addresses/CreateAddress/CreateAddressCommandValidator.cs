using FluentValidation;
using ERPServer.Application.Features.Addresses.CreateAddress;

namespace ERPServer.Application.Features.Addresses.CreateAddress
{
    public sealed class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
    {
        public CreateAddressCommandValidator()
        {
            // CustomerId: Must not be empty and the customer must exist
            RuleFor(o => o.CustomerId)
                .NotEmpty().WithMessage("Customer ID is required.");

            // AddressLine1: Must not be empty
            RuleFor(o => o.AddressLine1)
                .NotEmpty().WithMessage("Address Line 1 is required.");

            // AddressLine2: Optional, but if provided, it should not be longer than 250 characters
            RuleFor(o => o.AddressLine2)
                .MaximumLength(250).WithMessage("Address Line 2 cannot exceed 250 characters.");

            // City: Must not be empty
            RuleFor(o => o.City)
                .NotEmpty().WithMessage("City is required.");

            // State: Must not be empty
            RuleFor(o => o.State)
                .NotEmpty().WithMessage("State is required.");

            // ZipCode: Must not be empty
            RuleFor(o => o.ZipCode)
                .NotEmpty().WithMessage("ZipCode is required.");

            // Country: Must not be empty
            RuleFor(o => o.Country)
                .NotEmpty().WithMessage("Country is required.");
        }
    }
}
