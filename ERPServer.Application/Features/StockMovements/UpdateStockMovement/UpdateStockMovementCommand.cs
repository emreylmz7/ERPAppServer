using ERPServer.Domain.Enums;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.StockMovements.UpdateStockMovement;

public sealed record class UpdateStockMovementCommand(
    Guid Id,                // Unique identifier for the stock movement
    Guid? ProductId,        // Optional: Product ID for the stock movement
    DateTime? MovementDate, // Optional: Date of the stock movement
    int? Quantity,          // Optional: Quantity for the stock movement
    string Type,     // Optional: Type of movement (Inbound, Outbound)
    Guid? OrderId,          // Optional: Associated order ID
    Guid? SupplierId,       // Optional: Supplier ID
    string? Description,    // Optional: Description for the stock movement
    Guid? WarehouseId,      // Optional: Warehouse ID
    string Reason // Optional: Reason for the stock movement (Sale, Purchase, etc.)
) : IRequest<Result<string>>;
