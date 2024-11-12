using ERPServer.Domain.Abstractions;
using System;

namespace ERPServer.Domain.Entities;

public sealed class Customer : Entity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;      
    public string TaxOffice { get; set; } = string.Empty;          // Müşterinin vergi dairesi
    public string TaxNumber { get; set; } = string.Empty;          // Müşterinin vergi numarası
    public string City { get; set; } = string.Empty;               // Müşterinin bulunduğu şehir
    public string District { get; set; } = string.Empty;           // Müşterinin bulunduğu ilçe
    public string Address { get; set; } = string.Empty;      // Müşterinin adresi
    public string Email { get; set; } = string.Empty;              // Müşterinin e-posta adresi
    public string PhoneNumber { get; set; } = string.Empty;        // Müşterinin telefon numarası
    public string CustomerType { get; set; } = string.Empty;       // Müşteri tipi (örneğin, Bireysel, Kurumsal)
    public string Status { get; set; } = string.Empty;             // Müşterinin durumu (Aktif, Pasif vb.)
    public DateTime CreatedAt { get; set; } = DateTime.Now;   // Müşterinin oluşturulma tarihi
    public string CustomerGroup { get; set; } = string.Empty;          // Müşteri grubu (VIP, Normal vb.)
    public Guid UserId { get; set; } = Guid.Empty;
}
