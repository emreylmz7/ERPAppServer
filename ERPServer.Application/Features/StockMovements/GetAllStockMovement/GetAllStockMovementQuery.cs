using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.StockMovements.GetAllStockMovement;

public sealed record class GetAllStockMovementQuery() : IRequest<Result<List<GetAllStockMovementQueryResult>>>;
