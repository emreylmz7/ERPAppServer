using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Warehouses.GetAllWarehouse
{
    internal sealed class GetAllWarehouseQueryHandler : IRequestHandler<GetAllWarehouseQuery, Result<List<Warehouse>>>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IUserRepository _userRepository;

        public GetAllWarehouseQueryHandler(
            IWarehouseRepository warehouseRepository,
            IUserRepository userRepository)
        {
            _warehouseRepository = warehouseRepository;
            _userRepository = userRepository;
        }

        public async Task<Result<List<Warehouse>>> Handle(GetAllWarehouseQuery request, CancellationToken cancellationToken)
        {
            var userId = await _userRepository.GetCurrentUserId();
            if (userId == null)
            {
                return Result<List<Warehouse>>.Failure("User is not authenticated.");
            }

            List<Warehouse> warehouses = await _warehouseRepository
                .GetAll()
                .Where(w => w.CreatedBy == userId.Value)
                .OrderBy(w => w.WarehouseName)
                .ToListAsync(cancellationToken);

            return warehouses;
        }
    }
}
