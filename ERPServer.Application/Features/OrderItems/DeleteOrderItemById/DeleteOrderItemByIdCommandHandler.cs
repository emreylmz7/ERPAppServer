using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.OrderItems.DeleteOrderItemById;

internal sealed class DeleteOrderItemByIdCommandHandler : IRequestHandler<DeleteOrderItemByIdCommand, Result<string>>
{
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteOrderItemByIdCommandHandler(
        IOrderItemRepository orderItemRepository,
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _orderItemRepository = orderItemRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteOrderItemByIdCommand request, CancellationToken cancellationToken)
    {
        var orderItem = await _orderItemRepository.GetByExpressionAsync(oi => oi.Id == request.OrderItemId, cancellationToken);

        if (orderItem == null)
        {
            return Result<string>.Failure("Order Item Not Found.");
        }

        var product = await _productRepository.GetByExpressionAsync(p => p.Id == orderItem.ProductId, cancellationToken);
        if (product == null)
        {
            return Result<string>.Failure("Product Not Found.");

        }
        else
        {
            product.StockQuantity += orderItem.Quantity;
        }

        _productRepository.Update(product);
        _orderItemRepository.Delete(orderItem);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return "Order Item Deleted Successfully.";
    }
}
