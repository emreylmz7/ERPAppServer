using ERPServer.Domain.Entities;
using ERPServer.Domain.Enums;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Invoices.CreateInvoice;

public sealed record class CreateInvoiceCommand(
    Guid OrderId,                 // Sipariş ID'si
    string Status,         // Fatura durumu (default: Unpaid)
    DateTime? PaymentDate         // Opsiyonel ödeme tarihi
) : IRequest<Result<string>>;
