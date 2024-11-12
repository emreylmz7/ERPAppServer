using ERPServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Warehouses.GetAllWarehouse
{
    public sealed record class GetAllWarehouseQuery() : IRequest<Result<List<Warehouse>>>;
}
