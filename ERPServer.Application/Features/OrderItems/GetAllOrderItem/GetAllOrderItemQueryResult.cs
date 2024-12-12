using ERPServer.Domain.Abstractions;

namespace ERPServer.Application.Features.OrderItems.GetAllOrderItem
{
    public class GetAllOrderItemQueryResult : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal LineTotal { get; set; }
    }
}
