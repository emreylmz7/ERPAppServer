using ERPServer.Domain.Enums;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Invoices.UpdateInvoice;

public sealed record class UpdateInvoiceCommand(
    Guid Id,                      // Unique identifier for the invoice
    string Status,                 // Required: Status of the invoice (e.g., Paid, Unpaid)
    DateTime? PaymentDate        // Optional: Payment date for the invoice
) : IRequest<Result<string>>;
