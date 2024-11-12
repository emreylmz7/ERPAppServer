    using ERPServer.Domain.Entities;
    using MediatR;
    using TS.Result;

    namespace ERPServer.Application.Features.Categories.GetAllCategory;

    public sealed record class GetAllCategoryQuery() : IRequest<Result<List<Category>>>;
