using ERPServer.Domain.Entities;
using GenericRepository;

namespace ERPServer.Domain.Repositories
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        // İhtiyaç duyulacak özel metodlar buraya eklenebilir
        // Task<Invoice> GetInvoiceWithDetailsAsync(int invoiceId); // Faturayı detaylarıyla getirme
        // Task<List<Invoice>> GetInvoicesByCustomerIdAsync(int customerId); // Müşteriye ait faturaları getirme
        // Task<Invoice> GetInvoiceByOrderIdAsync(int orderId); // Siparişe ait faturayı getirme
    }
}
