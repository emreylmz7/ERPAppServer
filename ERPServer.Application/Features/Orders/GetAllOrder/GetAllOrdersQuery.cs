using ERPServer.Application.Features.Order.GetAllOrder;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Orders.GetAllOrders;

public sealed record class GetAllOrdersQuery() : IRequest<Result<List<GetAllOrderQueryResult>>>;
