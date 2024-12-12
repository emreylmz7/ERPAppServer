using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Orders.UpdateOrder;

internal sealed class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Result<string>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public UpdateOrderCommandHandler(
        IOrderRepository orderRepository,
        IOrderItemRepository orderItemRepository,
        ICustomerRepository customerRepository,
        IAddressRepository addressRepository,
        IProductRepository productRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IUserRepository userRepository)
    {
        _orderRepository = orderRepository;
        _orderItemRepository = orderItemRepository;
        _customerRepository = customerRepository;
        _addressRepository = addressRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<Result<string>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var userId = await _userRepository.GetCurrentUserId();
        if (userId == null)
        {
            return Result<string>.Failure("User is not authenticated.");
        }

        var order = await _orderRepository.GetAll()
                .Where(o => o.CreatedBy == userId.Value && o.Id == request.Id)
                .Include(o => o.Items)
                .FirstOrDefaultAsync(cancellationToken);

        if (order == null)
        {
            return Result<string>.Failure("Order not found.");
        }

        // Validate customer
        var customer = await _customerRepository.GetByExpressionAsync(p => p.Id == request.CustomerId, cancellationToken);
        if (customer == null)
        {
            return Result<string>.Failure("Customer not found.");
        }

        // Validate shipping address
        var shippingAddress = await _addressRepository.GetByExpressionAsync(p => p.Id == request.ShippingAddressId, cancellationToken);
        if (shippingAddress == null)
        {
            return Result<string>.Failure("Shipping address not found.");
        }

        order.ShippingAddressId = request.ShippingAddressId!.Value;
        order.UpdatedDate = DateTime.Now;

        // Remove all existing OrderItems
        foreach (var item in order.Items.ToList())
        {
            _orderItemRepository.Delete(item);
        }

        // Clear the Items collection in the order object
        order.Items.Clear();

        // Add new OrderItems
        foreach (var itemCommand in request.Items)
        {
            if (itemCommand.ProductId == null)
            {
                return Result<string>.Failure("Product ID cannot be null for Order Item.");
            }

            // Validate product
            var product = await _productRepository.GetByExpressionAsync(o => o.Id == itemCommand.ProductId, cancellationToken);
            if (product == null)
            {
                return Result<string>.Failure("Product not found for order item.");
            }

            var newOrderItem = new OrderItem
            {
                OrderId = order.Id,
                ProductId = itemCommand.ProductId.Value,
                Quantity = itemCommand.Quantity.GetValueOrDefault(),
                UnitPrice = product.Price,
                Discount = itemCommand.Discount.GetValueOrDefault(),
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            await _orderItemRepository.AddAsync(newOrderItem, cancellationToken);
            order.Items.Add(newOrderItem);
        }

        // Update order totals
        foreach (var item in order.Items)
        {
            item.CreatedBy = userId.Value;
        }
        order.PaymentMethod = request.PaymentMethod;
        order.Status = request.Status;
        order.SubTotal = order.Items.Sum(item => item.UnitPrice * item.Quantity);
        order.TaxAmount = order.SubTotal * order.TaxRate / 100;
        order.TotalAmount = order.SubTotal + order.TaxAmount + order.ShippingFee;

        // Update order
        _orderRepository.Update(order);

        // Save all changes
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return("Order Updated Successfully.");
    }
}