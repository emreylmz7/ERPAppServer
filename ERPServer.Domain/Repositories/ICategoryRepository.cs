using ERPServer.Domain.Entities;
using GenericRepository;

namespace ERPServer.Domain.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        /*Task<List<Category>> GetCategoriesWithProductsAsync();*/  // Örneğin, ürünlerle ilişkili kategorileri getirme
    }
}
