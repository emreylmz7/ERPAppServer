namespace ERPServer.Application.Features.StockMovements.GetAllStockMovement;

public class GetAllStockMovementQueryResult
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public DateTime MovementDate { get; set; }
    public int Quantity { get; set; }
    public string? Type { get; set; } // Enum: Inbound, Outbound
    public Guid? OrderId { get; set; } // Eğer bir sipariş ile bağlantılı ise
    public Guid? SupplierId { get; set; } // Eğer bir tedarikçi ile bağlantılı ise
    public string? Description { get; set; }
    public Guid WarehouseId { get; set; }
    public string WarehouseName { get; set; } = string.Empty;
    public string? Reason { get; set; } // Enum: Sale, Purchase, Adjustment, Transfer
}
