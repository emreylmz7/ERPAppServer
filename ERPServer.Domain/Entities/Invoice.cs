using ERPServer.Domain.Abstractions;
using ERPServer.Domain.Enums;

namespace ERPServer.Domain.Entities;

public class Invoice : BaseEntity
{
    public Guid OrderId { get; set; }
    public Order? Order { get; set; }
    public DateTime InvoiceDate { get; set; } = DateTime.Now;
    public decimal SubTotal { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal ShippingFee { get; set; }
    public decimal TotalAmount { get; set; }
    public InvoiceStatus Status { get; set; } = InvoiceStatus.Unpaid;
    public DateTime? PaymentDate { get; set; }
}


