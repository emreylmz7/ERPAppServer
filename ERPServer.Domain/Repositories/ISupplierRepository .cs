using ERPServer.Domain.Entities;
using GenericRepository;

namespace ERPServer.Domain.Repositories
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        // İhtiyaç duyulacak özel metodlar buraya eklenebilir
        //Task<List<Supplier>> GetSuppliersWithProductsAsync(); // Tedarikçileri ve onların sağladığı ürünleri getirme
        //Task<Supplier> GetSupplierWithContactsAsync(int supplierId); // Tedarikçiyi iletişim bilgileriyle beraber getirme
    }
}
