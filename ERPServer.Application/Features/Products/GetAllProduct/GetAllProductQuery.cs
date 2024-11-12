using ERPServer.Domain.Dtos.ProductDtos;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Products.GetAllProduct;

public sealed record class GetAllProductQuery() : IRequest<Result<List<ResultProductDto>>>;
