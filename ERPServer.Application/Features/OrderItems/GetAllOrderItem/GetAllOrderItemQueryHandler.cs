using AutoMapper;
using ERPServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;


namespace ERPServer.Application.Features.OrderItems.GetAllOrderItem
{
    internal sealed class GetAllOrderItemQueryHandler : IRequestHandler<GetAllOrderItemQuery, Result<List<GetAllOrderItemQueryResult>>>
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetAllOrderItemQueryHandler(IOrderItemRepository orderItemRepository, IOrderRepository orderRepository, IMapper mapper)
        {
            _orderItemRepository = orderItemRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllOrderItemQueryResult>>> Handle(GetAllOrderItemQuery request, CancellationToken cancellationToken)
        {
            var orderItems = await _orderItemRepository
                .GetAll()
                .Include(oi => oi.Product)
                .ToListAsync(cancellationToken);

            var orderItemDtos = _mapper.Map<List<GetAllOrderItemQueryResult>>(orderItems);
            return orderItemDtos;
        }
    }
}
