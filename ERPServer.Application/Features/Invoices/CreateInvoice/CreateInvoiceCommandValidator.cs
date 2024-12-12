using ERPServer.Domain.Enums;
using FluentValidation;

namespace ERPServer.Application.Features.Invoices.CreateInvoice;

public sealed class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
{
    public CreateInvoiceCommandValidator()
    {
        // OrderId: Must not be empty
        RuleFor(i => i.OrderId)
            .NotEmpty().WithMessage("Order ID is required.");

        // Status: Must be a valid InvoiceStatus value
        RuleFor(i => i.Status)
            .Must(status => Enum.TryParse<InvoiceStatus>(status.ToString(), out _))
            .WithMessage("Invalid invoice status. Must be a valid InvoiceStatus value.");

        // PaymentDate: Must be in the past or present (if provided)
        RuleFor(i => i.PaymentDate)
            .Must(paymentDate => !paymentDate.HasValue || paymentDate.Value <= DateTime.UtcNow)
            .WithMessage("Payment date must be in the past or present.");
    }
}
