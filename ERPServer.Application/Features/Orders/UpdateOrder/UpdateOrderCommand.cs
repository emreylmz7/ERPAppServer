using ERPServer.Application.Features.OrderItems.UpdateOrderItem;
using ERPServer.Domain.Enums;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Orders.UpdateOrder;

public sealed record class UpdateOrderCommand(
    Guid Id,             // Unique identifier for the order
    Guid? CustomerId,         // Optional: Customer ID for the order
    DateOnly? OrderDate,     // Optional: Date of the order
    decimal? ShippingFee,    // Optional: Shipping fee for the order
    decimal? SubTotal,    // Optional: Shipping fee for the order
    OrderStatus Status,           // Required: Status of the order (e.g., Pending, Shipped)
    Guid? ShippingAddressId,  // Optional: Shipping address ID
    PaymentMethod PaymentMethod, // Optional: Payment method (e.g., CreditCard, PayPal)
    DateTime? ShippingDate,  // Optional: Shipping date for the order
    DateTime? DeliveryDate , // Optional: Delivery date for the order
    List<UpdateOrderItemCommand> Items
) : IRequest<Result<string>>;
