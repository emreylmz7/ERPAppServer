using ERPServer.Domain.Abstractions;
using ERPServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.OrderItems.UpdateOrderItem;

public sealed record class UpdateOrderItemCommand(
    Guid? Id,                  // Unique identifier for the OrderItem
    Guid? ProductId,          // Optional: Product ID for the OrderItem
    Guid? OrderId,          // Optional: Order ID for the OrderItem
    int? Quantity,            // Optional: Quantity of the item
    decimal? UnitPrice,       // Optional: Unit price of the item
    decimal? Discount         // Optional: Discount on the item
) : IRequest<Result<string>>;

