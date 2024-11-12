using ERPServer.Domain.Abstractions;

namespace ERPServer.Domain.Entities;

public sealed class Warehouse:BaseEntity
{
    public string WarehouseName { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public List<StockMovement>? StockMovements { get; set; }
}
