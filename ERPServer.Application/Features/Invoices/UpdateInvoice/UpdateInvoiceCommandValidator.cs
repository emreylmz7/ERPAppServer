using ERPServer.Domain.Enums;
using FluentValidation;

namespace ERPServer.Application.Features.Invoices.UpdateInvoice
{
    public sealed class UpdateInvoiceCommandValidator : AbstractValidator<UpdateInvoiceCommand>
    {
        public UpdateInvoiceCommandValidator()
        {
            // Id: Fatura ID'si boş olamaz
            RuleFor(i => i.Id)
                .NotEmpty().WithMessage("Invoice ID is required.");

            // Status: Geçerli bir InvoiceStatus enum değeri olmalı
            RuleFor(i => i.Status)
                .Must(status => Enum.TryParse<InvoiceStatus>(status, out _))
                .WithMessage("Invalid invoice status. Must be a valid InvoiceStatus value.");

            // PaymentDate: Eğer sağlanmışsa, geçerli bir tarih olmalı
            RuleFor(i => i.PaymentDate)
                .NotEmpty().WithMessage("Payment date is required.")
                .When(i => i.PaymentDate.HasValue);
        }
    }
}
