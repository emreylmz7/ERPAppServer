using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.StockMovements.GetAllStockMovement
{
    internal sealed class GetAllStockMovementQueryHandler : IRequestHandler<GetAllStockMovementQuery, Result<List<GetAllStockMovementQueryResult>>>
    {
        private readonly IStockMovementRepository _stockMovementRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetAllStockMovementQueryHandler(IStockMovementRepository stockMovementRepository, IUserRepository userRepository, IMapper mapper)
        {
            _stockMovementRepository = stockMovementRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllStockMovementQueryResult>>> Handle(GetAllStockMovementQuery request, CancellationToken cancellationToken)
        {
            var userId = await _userRepository.GetCurrentUserId();
            if (userId == null)
            {
                return Result<List<GetAllStockMovementQueryResult>>.Failure("User is not authenticated.");
            }

            List<StockMovement> stockMovements = await _stockMovementRepository
                .GetAll()
                .Where(sm => sm.CreatedBy == userId.Value)
                .Include(p => p.Product)
                .Include(p => p.Warehouse)
                .OrderBy(sm => sm.MovementDate)  
                .ToListAsync(cancellationToken);

            List<GetAllStockMovementQueryResult> stockMovementsDTOs = _mapper.Map<List<GetAllStockMovementQueryResult>>(stockMovements);

            return stockMovementsDTOs;
        }
    }
}
