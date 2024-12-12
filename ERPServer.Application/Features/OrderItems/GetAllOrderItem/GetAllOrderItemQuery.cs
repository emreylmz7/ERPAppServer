using MediatR;
using TS.Result;
using System.Collections.Generic;

namespace ERPServer.Application.Features.OrderItems.GetAllOrderItem
{
    public sealed record class GetAllOrderItemQuery() : IRequest<Result<List<GetAllOrderItemQueryResult>>>;
}
