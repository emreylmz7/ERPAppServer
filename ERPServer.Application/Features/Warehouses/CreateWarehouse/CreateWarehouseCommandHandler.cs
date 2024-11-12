using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Warehouses.CreateWarehouse
{
    internal sealed class CreateWarehouseCommandHandler : IRequestHandler<CreateWarehouseCommand, Result<string>>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public CreateWarehouseCommandHandler(
            IWarehouseRepository warehouseRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IUserRepository userRepository)
        {
            _warehouseRepository = warehouseRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Result<string>> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
        {
            var userId = await _userRepository.GetCurrentUserId();
            if (userId == null)
            {
                return Result<string>.Failure("User is not authenticated.");
            }

            bool isWarehouseExist = await _warehouseRepository.AnyAsync(w => w.WarehouseName == request.WarehouseName, cancellationToken);
            if (isWarehouseExist)
            {
                return Result<string>.Failure("A warehouse with this name already exists.");
            }

            Warehouse warehouse = _mapper.Map<Warehouse>(request);
            warehouse.CreatedBy = userId.Value;

            await _warehouseRepository.AddAsync(warehouse, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return "Warehouse created successfully.";
        }
    }
}
