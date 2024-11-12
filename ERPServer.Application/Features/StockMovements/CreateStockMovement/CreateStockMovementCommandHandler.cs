using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Enums;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.StockMovements.CreateStockMovement;

internal sealed class CreateStockMovementCommandHandler : IRequestHandler<CreateStockMovementCommand, Result<string>>
{
    private readonly IStockMovementRepository _stockMovementRepository;
    private readonly IProductRepository _productRepository;
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public CreateStockMovementCommandHandler(
        IStockMovementRepository stockMovementRepository,
        IProductRepository productRepository,
        IWarehouseRepository warehouseRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IUserRepository userRepository)
    {
        _stockMovementRepository = stockMovementRepository;
        _productRepository = productRepository;
        _warehouseRepository = warehouseRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<Result<string>> Handle(CreateStockMovementCommand request, CancellationToken cancellationToken)
    {
        var userId = await _userRepository.GetCurrentUserId();
        if (userId == null)
        {
            return Result<string>.Failure("User is Not Authenticated.");
        }

        // Ürün doğrulama
        var product = await _productRepository.GetByExpressionAsync(p => p.Id == request.ProductId, cancellationToken);
        if (product == null)
        {
            return Result<string>.Failure("Product Not Found.");
        }

        // Depo doğrulama
        var warehouse = await _warehouseRepository.GetByExpressionAsync(wh => wh.Id == request.WarehouseId, cancellationToken);
        if (warehouse == null)
        {
            return Result<string>.Failure("Warehouse Not Found.");
        }

        // Stok hareketinin türüne göre işleme başla
        var stockMovement = _mapper.Map<StockMovement>(request);
        stockMovement.CreatedBy = userId.Value;

        // MovementType ve StockMovementReason kombinasyonunun geçerliliğini kontrol et
        if (!IsValidMovementReasonCombination(stockMovement.Type, stockMovement.Reason))
        {
            return Result<string>.Failure("Invalid Movement Type and Reason Combination.");
        }

        // İşlem türüne göre stok güncelleme
        switch (stockMovement.Type)
        {
            case MovementType.Inbound:
                if (stockMovement.Reason == StockMovementReason.Purchase || stockMovement.Reason == StockMovementReason.Return)
                {
                    // Satın alma veya iade işlemi: ürün stoğuna giriş
                    product.StockQuantity += request.Quantity;
                }
                break;

            case MovementType.Outbound:
                if (request.Quantity > product.StockQuantity)
                {
                    return Result<string>.Failure("Insufficient Stock For This Movement.");
                }

                if (stockMovement.Reason == StockMovementReason.Sale || stockMovement.Reason == StockMovementReason.Transfer)
                {
                    // Satış veya transfer işlemi: ürün stoğundan çıkış
                    product.StockQuantity -= request.Quantity;
                }
                break;

            default:
                return Result<string>.Failure("Invalid Movement Type.");
        }

        // Stok hareketi kaydet
        await _stockMovementRepository.AddAsync(stockMovement, cancellationToken);
        _productRepository.Update(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return "Stock Movement Created Successfully.";
    }

    private bool IsValidMovementReasonCombination(MovementType type, StockMovementReason reason)
    {
        // Geçerli kombinasyonlar: 
        // Inbound -> Purchase veya Return (Ürün girişi)
        // Outbound -> Sale veya Transfer (Ürün çıkışı)
        return (type == MovementType.Inbound && (reason == StockMovementReason.Purchase || reason == StockMovementReason.Return)) ||
               (type == MovementType.Outbound && (reason == StockMovementReason.Sale || reason == StockMovementReason.Transfer));
    }
}
