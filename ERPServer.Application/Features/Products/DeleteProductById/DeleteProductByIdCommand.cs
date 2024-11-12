using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Products.DeleteProductById;

public sealed record class DeleteProductByIdCommand(Guid Id) : IRequest<Result<string>>;
