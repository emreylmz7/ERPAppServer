using ERPServer.Domain.Abstractions;

namespace ERPServer.Domain.Entities;

public sealed class Product: BaseEntity
{
    public string SKU { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Cost { get; set; }
    public int StockQuantity { get; set; }
    public Guid CategoryId { get; set; }
    public Guid SupplierId { get; set; }
    public List<StockMovement>? StockMovements { get; set; } 
    public Category? Category { get; set; } 
    public Supplier? Supplier { get; set; } 
}
