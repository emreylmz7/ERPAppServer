using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Warehouses.DeleteWarehouseById
{
    public sealed record class DeleteWarehouseByIdCommand(Guid Id) : IRequest<Result<string>>;
}
