using AutoMapper;
using ERPServer.Domain.Dtos.ProductDtos;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Products.GetAllProduct
{
    internal sealed class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, Result<List<ResultProductDto>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetAllProductQueryHandler(IProductRepository productRepository, IUserRepository userRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<ResultProductDto>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var userId = await _userRepository.GetCurrentUserId();
            if (userId == null)
            {
                return Result<List<ResultProductDto>>.Failure("User is not authenticated.");
            }

            // Fetch products associated with the authenticated user
            List<Product> products = await _productRepository
                .GetAll()
                .Where(p => p.CreatedBy == userId.Value)
                .Include(p => p.Category)  
                .Include(p => p.Supplier)
                .OrderBy(p => p.Name)
                .ToListAsync(cancellationToken);

            List<ResultProductDto> productDTOs = _mapper.Map<List<ResultProductDto>>(products);

            return productDTOs;
        }
    }
}
