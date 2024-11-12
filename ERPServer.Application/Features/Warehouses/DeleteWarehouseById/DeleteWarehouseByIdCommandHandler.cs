using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Warehouses.DeleteWarehouseById
{
    internal sealed class DeleteWarehouseByIdCommandHandler : IRequestHandler<DeleteWarehouseByIdCommand, Result<string>>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteWarehouseByIdCommandHandler(IWarehouseRepository warehouseRepository, IUnitOfWork unitOfWork)
        {
            _warehouseRepository = warehouseRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(DeleteWarehouseByIdCommand request, CancellationToken cancellationToken)
        {
            // Attempt to retrieve the warehouse by its ID
            Warehouse warehouse = await _warehouseRepository.GetByExpressionAsync(w => w.Id == request.Id, cancellationToken);
            if (warehouse is null)
            {
                return Result<string>.Failure("Warehouse not found.");
            }

            // Delete the warehouse if it exists
            _warehouseRepository.Delete(warehouse);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return "Warehouse deleted successfully.";
        }
    }
}
