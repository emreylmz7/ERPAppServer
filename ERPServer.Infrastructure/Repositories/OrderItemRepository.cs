using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using ERPServer.Infrastructure.Context;
using GenericRepository;

namespace ERPServer.Infrastructure.Repositories
{
    internal sealed class OrderItemRepository : Repository<OrderItem, ApplicationDbContext>, IOrderItemRepository
    {
        public OrderItemRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Özel metodlar burada uygulanabilir
        // Örneğin: public async Task<List<OrderItem>> GetItemsByOrderIdAsync(int orderId) { ... }
    }
}
