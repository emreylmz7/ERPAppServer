using ERPServer.Domain.Entities;
using GenericRepository;
namespace ERPServer.Domain.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        // İhtiyaç duyulacak özel metodlar buraya eklenebilir
        // Task<Order> GetOrderWithDetailsAsync(int orderId); // Siparişi detayları ile getirme
        // Task<List<Order>> GetOrdersByCustomerIdAsync(int customerId); // Belirli bir müşteriye ait siparişleri getirme
        // Task<List<Order>> GetOrdersByStatusAsync(OrderStatus status); // Belirli bir duruma ait siparişleri getirme
    }
}
