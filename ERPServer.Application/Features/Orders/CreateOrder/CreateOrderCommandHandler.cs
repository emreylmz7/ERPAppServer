using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Orders.CreateOrder;

internal sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<string>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public CreateOrderCommandHandler(
        IOrderRepository orderRepository,
        ICustomerRepository customerRepository,
        IAddressRepository addressRepository,
        IProductRepository productRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IUserRepository userRepository)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _addressRepository = addressRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<Result<string>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var userId = await _userRepository.GetCurrentUserId();
        if (userId == null)
        {
            return Result<string>.Failure("User is Not Authenticated.");
        }

        // Müşteri doğrulama
        var customer = await _customerRepository.GetByExpressionAsync(p => p.Id == request.CustomerId, cancellationToken);
        if (customer == null)
        {
            return Result<string>.Failure("Customer Not Found.");
        }

        // Adres doğrulama
        var shippingAddress = await _addressRepository.GetByExpressionAsync(p => p.Id == request.ShippingAddressId, cancellationToken);
        if (shippingAddress == null)
        {
            return Result<string>.Failure("Shipping Address Not Found.");
        }

        // Sipariş oluşturma
        var order = _mapper.Map<Domain.Entities.Order>(request);
        order.CreatedBy = userId.Value;

        if (order.Items.Count > 0)
        {
            foreach (var item in order.Items)
            {
                var product = await _productRepository.GetByExpressionAsync(p => p.Id == item.ProductId, cancellationToken);
                if (product == null)
                {
                    return Result<string>.Failure("Product Not Found.");

                }
                else if (product.StockQuantity < item.Quantity)
                {
                    return Result<string>.Failure("Insufficient Product For This Order.");
                }
                item.UnitPrice = product.Price;
                item.CreatedBy = userId.Value;

                product.StockQuantity -= item.Quantity;
                _productRepository.Update(product);
            }
        }
        order.SubTotal = order.Items.Sum(item => item.UnitPrice * item.Quantity);
        order.TaxAmount = order.SubTotal * order.TaxRate / 100;
        order.TotalAmount = order.SubTotal + order.TaxAmount + order.ShippingFee;

        // Sipariş kaydet
        await _orderRepository.AddAsync(order, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return "Order Created Successfully.";
    }
}



//public decimal TotalAmount => Items.Sum(item => item.LineTotal) + TaxAmount + ShippingFee;
//public decimal SubTotal => Items.Sum(item => item.LineTotal);
//public decimal TaxAmount => SubTotal * (TaxRate / 100);