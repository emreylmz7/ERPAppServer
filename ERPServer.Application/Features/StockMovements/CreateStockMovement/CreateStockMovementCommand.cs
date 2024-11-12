using ERPServer.Domain.Entities;
using ERPServer.Domain.Enums;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.StockMovements.CreateStockMovement;

public sealed record class CreateStockMovementCommand(
    Guid ProductId,           // Product associated with the stock movement
    DateTime MovementDate,    // Date of the movement
    int Quantity,             // Quantity of stock being moved
    string Type,        // Type of movement: Inbound or Outbound
    Guid? OrderId,            // Optional Order ID if related to a specific order
    Guid? SupplierId,         // Optional Supplier ID if related to a supplier
    string? Description,      // Optional description for the movement
    Guid WarehouseId,         // Warehouse where the movement occurs
    string Reason // Reason for the stock movement (Sale, Purchase, Adjustment, Transfer)
) : IRequest<Result<string>>;
