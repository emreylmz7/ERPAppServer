using ERPServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.OrderItems.CreateOrderItem;

public sealed record class CreateOrderItemCommand(
    Guid ProductId,      // Product being ordered
    int Quantity,        // Quantity of the product  
    decimal Discount = 0 // Discount applied to the item
) : IRequest<Result<string>>;
