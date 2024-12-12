using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Invoices.DeleteInvoiceById;

internal sealed class DeleteInvoiceByIdCommandHandler : IRequestHandler<DeleteInvoiceByIdCommand, Result<string>>
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteInvoiceByIdCommandHandler(
        IInvoiceRepository invoiceRepository,
        IUnitOfWork unitOfWork)
    {
        _invoiceRepository = invoiceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteInvoiceByIdCommand request, CancellationToken cancellationToken)
    {
        // Invoice'ı bul
        Domain.Entities.Invoice invoice = await _invoiceRepository.GetByExpressionAsync(i => i.Id == request.InvoiceId, cancellationToken);

        if (invoice == null)
        {
            return Result<string>.Failure("Invoice Not Found.");
        }

        // Invoice'ı sil
        _invoiceRepository.Delete(invoice);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return "Invoice Deleted Successfully.";
    }
}
