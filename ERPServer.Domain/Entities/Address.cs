using ERPServer.Domain.Abstractions;

namespace ERPServer.Domain.Entities
{
    public class Address:BaseEntity
    {
        public Guid CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public string AddressLine1 { get; set; } = string.Empty;
        public string AddressLine2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}
