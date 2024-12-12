using ERPServer.Domain.Enums;
using FluentValidation;
using System;

namespace ERPServer.Application.Features.Addresses.UpdateAddress
{
    public sealed class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
    {
        public UpdateAddressCommandValidator()
        {
            // Id: Address ID is required (must not be empty)
            RuleFor(a => a.Id)
                .NotEmpty().WithMessage("Address ID is required.");

            // CustomerId: If provided, it must be a valid GUID
            RuleFor(a => a.CustomerId)
                .NotEmpty().WithMessage("Customer ID is required.")
                .When(a => a.CustomerId.HasValue);

            // AddressLine1: Address line 1 must not be empty
            RuleFor(a => a.AddressLine1)
                .NotEmpty().WithMessage("Address Line 1 is required.");

            // AddressLine2: Optional field, it can be null or empty, so no validation needed.

            // City: City must not be empty
            RuleFor(a => a.City)
                .NotEmpty().WithMessage("City is required.");

            // State: State must not be empty
            RuleFor(a => a.State)
                .NotEmpty().WithMessage("State is required.");

            // ZipCode: Zip code must not be empty and can be validated against a regex pattern (e.g., for standard zip code formats)
            RuleFor(a => a.ZipCode)
                .NotEmpty().WithMessage("ZipCode is required.")
                .Matches(@"^\d{5}(-\d{4})?$").WithMessage("ZipCode must be in a valid format (e.g., 12345 or 12345-6789).")
                .When(a => !string.IsNullOrEmpty(a.ZipCode));

            // Country: Country must not be empty
            RuleFor(a => a.Country)
                .NotEmpty().WithMessage("Country is required.");

            // Description: Optional field with a maximum length of 500 characters
            RuleFor(a => a.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.")
                .When(a => !string.IsNullOrEmpty(a.Description));
        }
    }
}
