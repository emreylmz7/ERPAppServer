using AutoMapper;
using ERPServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Invoices.GetAllInvoices
{
    internal sealed class GetAllInvoicesQueryHandler : IRequestHandler<GetAllInvoicesQuery, Result<List<GetAllInvoiceQueryResult>>>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllInvoicesQueryHandler(IInvoiceRepository invoiceRepository, IUserRepository userRepository, IMapper mapper)
        {
            _invoiceRepository = invoiceRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllInvoiceQueryResult>>> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
        {
            var userId = await _userRepository.GetCurrentUserId();
            if (userId == null)
            {
                return Result<List<GetAllInvoiceQueryResult>>.Failure("User Is Not Authenticated.");
            }

            List<Domain.Entities.Invoice> invoices = await _invoiceRepository
                .GetAll()
                .Where(i => i.CreatedBy == userId.Value)  // Kullanıcıya ait faturaları filtrele
                .Include(i => i.Order)  // Siparişle ilişkili veriyi dahil et
                .Include(i => i.Order!.Customer)  // Müşteri bilgilerini dahil et
                .OrderBy(i => i.InvoiceDate)  // Fatura tarihine göre sırala
                .ToListAsync(cancellationToken);

            List<GetAllInvoiceQueryResult> invoiceDtos = _mapper.Map<List<GetAllInvoiceQueryResult>>(invoices);

            return invoiceDtos;
        }
    }
}
