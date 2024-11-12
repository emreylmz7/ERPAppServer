using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Warehouses.UpdateWarehouse
{
    internal sealed class UpdateWarehouseCommandHandler : IRequestHandler<UpdateWarehouseCommand, Result<string>>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateWarehouseCommandHandler(
            IWarehouseRepository warehouseRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _warehouseRepository = warehouseRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<string>> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the existing warehouse by ID
            var warehouse = await _warehouseRepository.GetByExpressionAsync(w => w.Id == request.Id, cancellationToken);
            if (warehouse == null)
            {
                return Result<string>.Failure("Warehouse not found.");
            }

            // Map updated fields to the existing warehouse object
            _mapper.Map(request, warehouse);
            warehouse.UpdatedDate = DateTime.Now;

            _warehouseRepository.Update(warehouse);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return "Warehouse updated successfully.";
        }
    }
}
