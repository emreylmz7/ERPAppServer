using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Customers.UpdateCustomer;

public sealed record class UpdateCustomerCommand(
    Guid Id,        // Unique identifier of the customer
    string? FirstName,        // Optional: Müşterinin adı
    string? LastName,         // Optional: Müşterinin soyadı
    string? TaxOffice,        // Optional: Müşterinin vergi dairesi
    string? TaxNumber,        // Optional: Müşterinin vergi numarası
    string? City,             // Optional: Müşterinin bulunduğu şehir
    string? District,         // Optional: Müşterinin bulunduğu ilçe
    string? Address,          // Optional: Müşterinin tam adresi
    string? Email,            // Optional: Müşterinin e-posta adresi
    string? PhoneNumber,      // Optional: Müşterinin telefon numarası
    string? CustomerType,     // Optional: Müşteri tipi (Bireysel, Kurumsal)
    string? Status,           // Optional: Müşterinin durumu (Aktif, Pasif vb.)
    string? CustomerGroup     // Optional: Müşteri grubu (VIP, Normal vb.)
) : IRequest<Result<string>>;
