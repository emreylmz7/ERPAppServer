using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Orders.DeleteOrderById;

internal sealed class DeleteOrderByIdCommandHandler : IRequestHandler<DeleteOrderByIdCommand, Result<string>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteOrderByIdCommandHandler(
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteOrderByIdCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Order order = await _orderRepository.GetByExpressionAsync(o => o.Id == request.OrderId, cancellationToken);

        if (order == null)
        {
            return Result<string>.Failure("Order Not Found.");
        }

        _orderRepository.Delete(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return "Order Deleted Successfully.";
    }
}
