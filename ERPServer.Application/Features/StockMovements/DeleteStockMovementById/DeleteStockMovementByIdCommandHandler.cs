using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.StockMovements.DeleteStockMovementById;

internal sealed class DeleteStockMovementByIdCommandHandler : IRequestHandler<DeleteStockMovementByIdCommand, Result<string>>
{
    private readonly IStockMovementRepository _stockMovementRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteStockMovementByIdCommandHandler(
        IStockMovementRepository stockMovementRepository,
        IUnitOfWork unitOfWork)
    {
        _stockMovementRepository = stockMovementRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteStockMovementByIdCommand request, CancellationToken cancellationToken)
    {
        StockMovement stockMovement = await _stockMovementRepository.GetByExpressionAsync(sm => sm.Id == request.Id, cancellationToken);

        if (stockMovement == null)
        {
            return Result<string>.Failure("Stock movement not found.");
        }

        _stockMovementRepository.Delete(stockMovement);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return "Stock movement deleted successfully.";
    }
}
