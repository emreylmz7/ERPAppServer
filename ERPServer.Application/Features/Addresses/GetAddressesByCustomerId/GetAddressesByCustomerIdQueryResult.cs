using ERPServer.Domain.Abstractions;

namespace ERPServer.Application.Features.Addresses.GetAddressesByCustomerId;

public class GetAddressesByCustomerIdQueryResult : BaseEntity
{
    public string CustomerName { get; set; } = string.Empty; // Customer Name related to the address
    public string AddressLine1 { get; set; } = string.Empty; // Address Line 1
    public string AddressLine2 { get; set; } = string.Empty; // Address Line 2
    public string City { get; set; } = string.Empty;          // City
    public string State { get; set; } = string.Empty;         // State
    public string ZipCode { get; set; } = string.Empty;      // ZipCode
    public string Country { get; set; } = string.Empty;      // Country
}
