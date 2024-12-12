using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.OrderItems.UpdateOrderItem;

internal sealed class UpdateOrderItemCommandHandler : IRequestHandler<UpdateOrderItemCommand, Result<string>>
{
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateOrderItemCommandHandler(
        IOrderItemRepository orderItemRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _orderItemRepository = orderItemRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(UpdateOrderItemCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the OrderItem by ID
        var orderItem = await _orderItemRepository.GetByExpressionAsync(oi => oi.Id == request.Id, cancellationToken);
        if (orderItem == null)
        {
            return Result<string>.Failure("Order Item Not Found.");
        }

        // Map the changes from request to the existing OrderItem
        _mapper.Map(request, orderItem);

        // Set the updated date/time
        orderItem.UpdatedDate = DateTime.Now;

        // Update the repository and save changes
        _orderItemRepository.Update(orderItem);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return "Order Item Updated Successfully.";
    }
}
