using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Addresses.CreateAddress
{
    public sealed record class CreateAddressCommand(
        Guid CustomerId,           // Customer ID associated with the address
        string AddressLine1,       // First line of the address
        string AddressLine2,       // Second line of the address (optional)
        string City,               // City of the address
        string State,              // State of the address
        string ZipCode,            // ZipCode of the address
        string Country             // Country of the address
    ) : IRequest<Result<string>>;  // The response is a string (success message) or failure message
}
