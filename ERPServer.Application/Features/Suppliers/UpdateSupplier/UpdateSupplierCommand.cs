using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Suppliers.UpdateSupplier
{
    public sealed record class UpdateSupplierCommand(
        Guid Id,
        string? CompanyName,
        string? ContactName,
        string? ContactEmail,
        string? ContactPhone,
        string? Address
    ) : IRequest<Result<string>>;
}
