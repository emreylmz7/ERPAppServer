using ERPServer.Application.Features.OrderItems.CreateOrderItem;
using ERPServer.Domain.Enums;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Orders.CreateOrder;

public sealed record class CreateOrderCommand(
    Guid CustomerId,                 // Siparişin ilişkili olduğu müşteri ID'si
    Guid ShippingAddressId,          // Gönderim adresi ID'si
    OrderStatus Status,             // Sipariş durumu (default: Pending)
    decimal TaxRate,              // Vergi tutarı
    decimal ShippingFee,            // Kargo ücreti
    PaymentMethod PaymentMethod,    // Ödeme yöntemi
    DateTime? ShippingDate,         // Opsiyonel gönderim tarihi
    DateTime? DeliveryDate,   // Opsiyonel teslim tarihi
    List<CreateOrderItemCommand> Items
) : IRequest<Result<string>>;
