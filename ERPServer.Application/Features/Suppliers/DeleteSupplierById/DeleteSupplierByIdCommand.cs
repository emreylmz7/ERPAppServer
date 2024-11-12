using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Suppliers.DeleteSupplierById
{
    public sealed record class DeleteSupplierByIdCommand(Guid Id) : IRequest<Result<string>>;
}
