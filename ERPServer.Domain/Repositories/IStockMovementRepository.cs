using ERPServer.Domain.Entities;
using GenericRepository;

namespace ERPServer.Domain.Repositories
{
    public interface IStockMovementRepository : IRepository<StockMovement>
    {
        // İhtiyaç duyulacak özel metodlar buraya eklenebilir
        //Task<List<StockMovement>> GetStockMovementsByProductAsync(int productId);  // Bir ürüne ait stok hareketlerini getirme
        //Task<List<StockMovement>> GetStockMovementsByDateRangeAsync(DateTime startDate, DateTime endDate); // Belirli bir tarih aralığındaki stok hareketlerini getirme
        //Task<List<StockMovement>> GetStockMovementsByWarehouseAsync(int warehouseId); // Belirli bir depoya ait stok hareketlerini getirme
    }
}
