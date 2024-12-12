using ERPServer.Domain.Abstractions;

namespace ERPServer.Domain.Entities;

public class OrderItem : BaseEntity
{
    public Guid OrderId { get; set; }
    public Order? Order { get; set; }
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; } = 0;
    public decimal LineTotal => (UnitPrice - Discount) * Quantity;
}
