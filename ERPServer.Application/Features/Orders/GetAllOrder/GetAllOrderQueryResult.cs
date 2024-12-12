using ERPServer.Application.Features.OrderItems.GetAllOrderItem;
using ERPServer.Domain.Abstractions;
using ERPServer.Domain.Enums;

namespace ERPServer.Application.Features.Order.GetAllOrder
{
    public class GetAllOrderQueryResult : BaseEntity
    {
        public DateOnly OrderDate { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty; 
        public string Address { get; set; } = string.Empty; 
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxRate { get; set; }
        public decimal ShippingFee { get; set; }                                                                                                                            
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime? ShippingDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public List<GetAllOrderItemQueryResult>? Items { get; set; }
    }
}
