using ERPServer.Domain.Entities;
using GenericRepository;

namespace ERPServer.Domain.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        // İhtiyaç duyulacak özel metodlar buraya eklenebilir
        //Task<Product> GetProductWithCategoryAndSupplierAsync(int productId); // Ürünle kategori ve tedarikçiyi getirme
        //Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId); // Belirli bir kategoriye ait ürünleri getirme
        //Task<List<Product>> GetLowStockProductsAsync(int threshold); // Düşük stoklu ürünleri getirme
    }
}
