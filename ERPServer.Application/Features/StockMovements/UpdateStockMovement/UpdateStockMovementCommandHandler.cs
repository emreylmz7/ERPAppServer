using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.StockMovements.UpdateStockMovement;

internal sealed class UpdateStockMovementCommandHandler : IRequestHandler<UpdateStockMovementCommand, Result<string>>
{
    private readonly IStockMovementRepository _stockMovementRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateStockMovementCommandHandler(
        IStockMovementRepository stockMovementRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _stockMovementRepository = stockMovementRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(UpdateStockMovementCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the stock movement by its ID
        var stockMovement = await _stockMovementRepository.GetByExpressionAsync(sm => sm.Id == request.Id, cancellationToken);
        if (stockMovement == null)
        {
            return Result<string>.Failure("Stock movement not found.");
        }

        // Map the updated fields to the existing stock movement entity
        _mapper.Map(request, stockMovement);
        stockMovement.UpdatedDate = DateTime.Now;

        // Update the stock movement in the repository
        _stockMovementRepository.Update(stockMovement);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return "Stock movement updated successfully.";
    }
}
