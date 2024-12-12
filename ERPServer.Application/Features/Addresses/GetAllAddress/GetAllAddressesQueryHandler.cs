using AutoMapper;
using ERPServer.Application.Features.Addresses.GetAllAddress;
using ERPServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Addresses.GetAllAddresses
{
    internal sealed class GetAllAddressesQueryHandler : IRequestHandler<GetAllAddressesQuery, Result<List<GetAllAddressQueryResult>>>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllAddressesQueryHandler(IAddressRepository addressRepository, IUserRepository userRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllAddressQueryResult>>> Handle(GetAllAddressesQuery request, CancellationToken cancellationToken)
        {
            // Get the current user ID
            var userId = await _userRepository.GetCurrentUserId();
            if (userId == null)
            {
                return Result<List<GetAllAddressQueryResult>>.Failure("User is Not Authenticated.");
            }

            // Get all addresses related to the user (filter by user ID)
            List<Domain.Entities.Address> addresses = await _addressRepository
                .GetAll()
                .Where(a => a.CreatedBy == userId.Value) // Only retrieve addresses belonging to the current user
                .Include(a => a.Customer)                  // Including customer details for the Address
                .OrderBy(a => a.City)                      // Optional: Sort by city or other criteria
                .ToListAsync(cancellationToken);

            // Map the retrieved addresses to the result DTO
            List<GetAllAddressQueryResult> addressDtos = _mapper.Map<List<GetAllAddressQueryResult>>(addresses);

            // Return the mapped address DTOs
            return addressDtos;
        }
    }
}
