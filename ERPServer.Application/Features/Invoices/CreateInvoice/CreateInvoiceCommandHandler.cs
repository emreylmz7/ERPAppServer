using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Invoices.CreateInvoice;

internal sealed class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, Result<string>>
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public CreateInvoiceCommandHandler(
        IInvoiceRepository invoiceRepository,
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IUserRepository userRepository)
    {
        _invoiceRepository = invoiceRepository;
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<Result<string>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        // Kullanıcı doğrulaması
        var userId = await _userRepository.GetCurrentUserId();
        if (userId == null)
        {
            return Result<string>.Failure("User is Not Authenticated.");
        }

        var order = await _orderRepository.GetByExpressionAsync(p=> p.Id == request.OrderId, cancellationToken);

        if (order == null)
        {
            return Result<string>.Failure("Order Not Found.");
        }

        var invoice = _mapper.Map<Invoice>(request);
        invoice.InvoiceDate = DateTime.Now;
        invoice.ShippingFee = order.ShippingFee;
        invoice.SubTotal = order.SubTotal;
        invoice.TotalAmount = order.TotalAmount;
        invoice.TaxAmount = order.TaxAmount;
        invoice.CreatedBy = userId.Value;

        await _invoiceRepository.AddAsync(invoice, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return "Invoice Created Successfully.";
    }
}
