using ERPServer.Domain.Abstractions;
using ERPServer.Domain.Enums;

namespace ERPServer.Domain.Entities;

public sealed class StockMovement : BaseEntity
{
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
    public DateTime MovementDate { get; set; }
    public int Quantity { get; set; }
    public MovementType Type { get; set; } // Enum: Inbound, Outbound
    public Guid? OrderId { get; set; } // Eğer bir sipariş ile bağlantılı ise
    public Guid? SupplierId { get; set; } // Eğer bir tedarikçi ile bağlantılı ise
    public string? Description { get; set; }
    public Guid WarehouseId { get; set; }
    public Warehouse? Warehouse { get; set; }
    public StockMovementReason Reason { get; set; } // Enum: Sale, Purchase, Adjustment, Transfer
}
