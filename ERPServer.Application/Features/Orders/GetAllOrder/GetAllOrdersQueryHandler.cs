using AutoMapper;
using ERPServer.Application.Features.Order.GetAllOrder;
using ERPServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Orders.GetAllOrders
{
    internal sealed class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, Result<List<GetAllOrderQueryResult>>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllOrdersQueryHandler(IOrderRepository orderRepository, IUserRepository userRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllOrderQueryResult>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var userId = await _userRepository.GetCurrentUserId();
            if (userId == null)
            {
                return Result<List<GetAllOrderQueryResult>>.Failure("User Is Not Authenticated.");
            }

            List<Domain.Entities.Order> orders = await _orderRepository
                .GetAll()
                .Where(o => o.CreatedBy == userId.Value)
                .Include(o => o.Customer)
                .Include(o => o.ShippingAddress)
                .Include(o => o.Items)
                    .ThenInclude(oi => oi.Product)
                .OrderBy(o => o.OrderDate)
                .ToListAsync(cancellationToken);

            List<GetAllOrderQueryResult> orderDtos = _mapper.Map<List<GetAllOrderQueryResult>>(orders);

            return orderDtos;
        }
    }
}