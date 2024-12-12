using AutoMapper;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Invoices.UpdateInvoice
{
    internal sealed class UpdateInvoiceCommandHandler : IRequestHandler<UpdateInvoiceCommand, Result<string>>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateInvoiceCommandHandler(
            IInvoiceRepository invoiceRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _invoiceRepository = invoiceRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<string>> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
        {
            // Faturayı al
            var invoice = await _invoiceRepository.GetByExpressionAsync(i => i.Id == request.Id, cancellationToken);
            if (invoice == null)
            {
                return Result<string>.Failure("Invoice Not Found.");
            }

            // İlgili alanları güncelle
            _mapper.Map(request, invoice);
            invoice.UpdatedDate = DateTime.Now;  // Güncelleme tarihi ekle

            // Faturayı güncelle
            _invoiceRepository.Update(invoice);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return "Invoice Updated Successfully.";
        }
    }
}
