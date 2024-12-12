using MediatR;
using System;
using TS.Result;

namespace ERPServer.Application.Features.Addresses.UpdateAddress
{
    public sealed record class UpdateAddressCommand(
        Guid Id,                     // Unique identifier for the address
        Guid? CustomerId,             // Optional: Customer ID related to the address
        string AddressLine1,          // Required: Address Line 1
        string AddressLine2,          // Optional: Address Line 2
        string City,                  // Required: City
        string State,                 // Required: State
        string ZipCode,               // Required: ZipCode
        string Country,               // Required: Country
        string? Description           // Optional: Description for the address
    ) : IRequest<Result<string>>;    // Returns a result, indicating success or failure
}
