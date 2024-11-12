using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Categories.CreateCategory;

public sealed record class CreateCategoryCommand(
    string Name,             
    string Description
) : IRequest<Result<string>>;
