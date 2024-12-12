using AutoMapper;
using ERPServer.Domain.Repositories;
using MediatR;
using TS.Result;
using Microsoft.EntityFrameworkCore;

namespace ERPServer.Application.Features.Addresses.GetAddressesByCustomerId
{
    internal sealed class GetAddressesByCustomerIdQueryHandler : IRequestHandler<GetAddressesByCustomerIdQuery, Result<List<GetAddressesByCustomerIdQueryResult>>>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAddressesByCustomerIdQueryHandler(IAddressRepository addressRepository, IUserRepository userRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAddressesByCustomerIdQueryResult>>> Handle(GetAddressesByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var userId = await _userRepository.GetCurrentUserId();
            if (userId == null)
            {
                return Result<List<GetAddressesByCustomerIdQueryResult>>.Failure("User is Not Authenticated.");
            }

            List<Domain.Entities.Address> addresses = await _addressRepository
                .GetAll()
                .Where(a => a.CreatedBy == userId.Value && a.CustomerId == request.CustomerId) 
                .Include(a => a.Customer)                 
                .OrderBy(a => a.City)                      
                .ToListAsync(cancellationToken);

 
            List<GetAddressesByCustomerIdQueryResult> addressDtos = _mapper.Map<List<GetAddressesByCustomerIdQueryResult>>(addresses);

            return addressDtos;
        }
    }
}
