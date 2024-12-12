using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using ERPServer.Infrastructure.Context;
using GenericRepository;

namespace ERPServer.Infrastructure.Repositories
{
    internal sealed class InvoiceRepository : Repository<Invoice, ApplicationDbContext>, IInvoiceRepository
    {
        public InvoiceRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Özel metodlar burada uygulanabilir
        // Örneğin: public async Task<Invoice> GetInvoiceWithDetailsAsync(int invoiceId) { ... }
    }
}
