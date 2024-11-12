using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Suppliers.DeleteSupplierById
{
    internal sealed class DeleteSupplierByIdCommandHandler : IRequestHandler<DeleteSupplierByIdCommand, Result<string>>
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSupplierByIdCommandHandler(ISupplierRepository supplierRepository, IUnitOfWork unitOfWork)
        {
            _supplierRepository = supplierRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(DeleteSupplierByIdCommand request, CancellationToken cancellationToken)
        {
            // Attempt to retrieve the supplier by its ID
            Supplier supplier = await _supplierRepository.GetByExpressionAsync(s => s.Id == request.Id, cancellationToken);
            if (supplier is null)
            {
                return Result<string>.Failure("Supplier not found");
            }

            // Delete the supplier if it exists
            _supplierRepository.Delete(supplier);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return "Supplier deleted successfully";
        }
    }
}
