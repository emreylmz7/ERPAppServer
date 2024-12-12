using ERPServer.Domain.Entities;
using GenericRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPServer.Domain.Repositories
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        // İhtiyaç duyulacak özel metodlar buraya eklenebilir
        // Task<List<OrderItem>> GetItemsByOrderIdAsync(int orderId); // Siparişe ait ürünleri getirme
        // Task<OrderItem> GetItemWithProductAsync(int orderItemId); // Ürün detaylarıyla bir sipariş kalemi getirme
    }
}
