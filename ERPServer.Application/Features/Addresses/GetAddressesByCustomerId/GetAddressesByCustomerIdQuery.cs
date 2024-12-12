using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Addresses.GetAddressesByCustomerId;

public sealed record class GetAddressesByCustomerIdQuery(Guid CustomerId) : IRequest<Result<List<GetAddressesByCustomerIdQueryResult>>>;


