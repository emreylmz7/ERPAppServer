using ERPServer.Domain.Abstractions;
using ERPServer.Domain.Enums;

namespace ERPServer.Domain.Entities;

public class Order : BaseEntity
{
    public DateOnly OrderDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public Guid CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public Guid ShippingAddressId { get; set; }
    public Address? ShippingAddress { get; set; }
    public decimal TaxRate { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public decimal TotalAmount { get; set; }
    public decimal SubTotal { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal ShippingFee { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public DateTime? ShippingDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public List<OrderItem> Items { get; set; } = new List<OrderItem>();
}



