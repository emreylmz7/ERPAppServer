using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;
using Microsoft.EntityFrameworkCore;


namespace ERPServer.Application.Features.OrderItems.CreateOrderItem;

internal sealed class CreateOrderItemCommandHandler : IRequestHandler<CreateOrderItemCommand, Result<string>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateOrderItemCommandHandler(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IOrderItemRepository orderItemRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _orderItemRepository = orderItemRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
    {
        var userId = await _userRepository.GetCurrentUserId();
        if (userId == null)
        {
            return Result<string>.Failure("User is Not Authenticated.");
        }

        //// Siparişi bul
        //var order = await _orderRepository.GetByExpressionAsync(p => p.Id == request.OrderId, cancellationToken);
        //if (order == null)
        //{
        //    return Result<string>.Failure("Order Not Found.");
        //}

        // Ürünü bul
        var product = await _productRepository.GetByExpressionAsync(p => p.Id == request.ProductId, cancellationToken);
        if (product == null)
        {
            return Result<string>.Failure("Product Not Found.");

        }else if (product.StockQuantity < request.Quantity)
        {
            return Result<string>.Failure("Insufficient Product For This Order.");
        }

        // OrderItem'ı oluştur
        var orderItem = _mapper.Map<OrderItem>(request);
        orderItem.CreatedBy = userId.Value;
        orderItem.UnitPrice = product.Price;

        // OrderItem'ı kaydet
        await _orderItemRepository.AddAsync(orderItem, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        //var orderItems = await _orderItemRepository.GetAll().Where(oi => oi.OrderId == order.Id).ToListAsync();
        //order.SubTotal = orderItems.Sum(item => item.UnitPrice * item.Quantity);
        //order.TaxAmount = order.SubTotal * order.TaxRate / 100;
        //order.TotalAmount = order.SubTotal + order.TaxAmount + order.ShippingFee;

        //_orderRepository.Update(order);

        product.StockQuantity -= request.Quantity;
        _productRepository.Update(product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return "Order Item Created Successfully and Order Updated.";
    }

}
