using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Addresses.DeleteAddressById
{
    public sealed record class DeleteAddressByIdCommand(Guid AddressId) : IRequest<Result<string>>;
}
