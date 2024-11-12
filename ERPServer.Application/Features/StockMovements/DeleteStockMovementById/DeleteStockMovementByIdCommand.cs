using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.StockMovements.DeleteStockMovementById;

public sealed record class DeleteStockMovementByIdCommand(Guid Id) : IRequest<Result<string>>;
