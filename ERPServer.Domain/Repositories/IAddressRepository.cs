using ERPServer.Domain.Entities;
using GenericRepository;

namespace ERPServer.Domain.Repositories
{
    public interface IAddressRepository : IRepository<Address>
    {
        // İhtiyaç duyulacak özel metodlar buraya eklenebilir
        // Task<Address> GetAddressWithCustomerAsync(int addressId); // Adres ile ilişkili müşteri getirme
        // Task<List<Address>> GetAddressesByCustomerIdAsync(int customerId); // Müşteriye ait adresleri getirme
    }
}
