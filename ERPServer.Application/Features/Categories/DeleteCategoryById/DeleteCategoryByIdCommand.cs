using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Categories.DeleteCategoryById;

public sealed record class DeleteCategoryByIdCommand(Guid Id) : IRequest<Result<string>>;
