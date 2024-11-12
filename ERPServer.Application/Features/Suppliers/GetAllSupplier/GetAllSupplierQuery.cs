using ERPServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Suppliers.GetAllSupplier
{
    public sealed record class GetAllSupplierQuery() : IRequest<Result<List<Supplier>>>;
}
