using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Customers.CreateCustomer;

public sealed record class CreateCustomerCommand(
    string FirstName,         // Müşterinin adı
    string LastName,          // Müşterinin soyadı
    string TaxOffice,         // Müşterinin vergi dairesi
    string TaxNumber,         // Müşterinin vergi numarası
    string City,              // Müşterinin bulunduğu şehir
    string District,          // Müşterinin bulunduğu ilçe
    string Address,           // Müşterinin tam adresi (renamed from StreetAddress)
    string Email,             // Müşterinin e-posta adresi
    string PhoneNumber,       // Müşterinin telefon numarası
    string CustomerType,      // Müşteri tipi (Bireysel, Kurumsal)
    string Status,            // Müşterinin durumu (Aktif, Pasif vb.)
    string CustomerGroup      // Müşteri grubu (VIP, Normal vb.)
) : IRequest<Result<string>>;



