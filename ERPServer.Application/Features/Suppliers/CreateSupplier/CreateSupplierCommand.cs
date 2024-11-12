using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Suppliers.CreateSupplier
{
    public sealed record class CreateSupplierCommand(
        string CompanyName,
        string ContactName,
        string ContactEmail,
        string ContactPhone,
        string Address
    ) : IRequest<Result<string>>;
}
