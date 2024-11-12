using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Suppliers.UpdateSupplier
{
    internal sealed class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand, Result<string>>
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateSupplierCommandHandler(
            ISupplierRepository supplierRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<string>> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the existing supplier by ID
            var supplier = await _supplierRepository.GetByExpressionAsync(s => s.Id == request.Id, cancellationToken);
            if (supplier == null)
            {
                return Result<string>.Failure("Supplier not found.");
            }

            // Map updated fields to the existing supplier object
            _mapper.Map(request, supplier);
            supplier.UpdatedDate = DateTime.Now;

            _supplierRepository.Update(supplier);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return "Supplier updated successfully.";
        }
    }
}
