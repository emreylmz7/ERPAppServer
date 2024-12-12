using ERPServer.Domain.Abstractions;

namespace ERPServer.Application.Features.Invoices.GetAllInvoices;

public class GetAllInvoiceQueryResult: BaseEntity
{
    public DateTime InvoiceDate { get; set; }  // Fatura tarihi
    public Guid OrderId { get; set; }  // İlgili Sipariş ID'si
    public decimal TotalAmount { get; set; }  // Toplam tutar
    public decimal TaxAmount { get; set; }  // Vergi tutarı
    public decimal SubTotal { get; set; }
    public decimal ShippingFee { get; set; }
    public string Status { get; set; } = string.Empty;  // Fatura durumu
    public DateTime? PaymentDate { get; set; }  // Ödeme tarihi





    

}
