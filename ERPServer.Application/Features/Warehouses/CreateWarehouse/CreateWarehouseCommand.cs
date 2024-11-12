using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Warehouses.CreateWarehouse
{
    public sealed record class CreateWarehouseCommand(
        string WarehouseName,
        string Location
    ) : IRequest<Result<string>>;
}
