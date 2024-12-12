using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using ERPServer.Infrastructure.Context;
using GenericRepository;

namespace ERPServer.Infrastructure.Repositories
{
    internal sealed class AddressRepository : Repository<Address, ApplicationDbContext>, IAddressRepository
    {
        public AddressRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Özel metodlar burada uygulanabilir
        // Örneğin: public async Task<Address> GetAddressWithCustomerAsync(int addressId) { ... }
    }
}
    