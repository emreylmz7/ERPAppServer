using ERPServer.Application.Features.Addresses.GetAllAddress;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Addresses.GetAllAddresses
{
    public sealed record class GetAllAddressesQuery() : IRequest<Result<List<GetAllAddressQueryResult>>>;
}
