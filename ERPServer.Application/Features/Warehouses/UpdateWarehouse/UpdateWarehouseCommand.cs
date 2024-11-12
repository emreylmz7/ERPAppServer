using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Warehouses.UpdateWarehouse
{
    public sealed record class UpdateWarehouseCommand(
        Guid Id,
        string? WarehouseName,
        string? Location
    ) : IRequest<Result<string>>;
}
