using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Addresses.CreateAddress
{
    internal sealed class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, Result<string>>
    {
        private readonly IAddressRepository _addressRepository;  // Repository to handle address CRUD operations
        private readonly ICustomerRepository _customerRepository; // Repository to validate customer
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;  // Unit of Work for transaction management
        private readonly IMapper _mapper;

        public CreateAddressCommandHandler(IAddressRepository addressRepository, ICustomerRepository customerRepository, IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _customerRepository = customerRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<string>> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            var userId = await _userRepository.GetCurrentUserId();
            if (userId == null)
            {
                return Result<string>.Failure("User is not authenticated.");
            }
            // Customer Validation
            var customer = await _customerRepository.GetByExpressionAsync(c => c.Id == request.CustomerId, cancellationToken);
            if (customer == null)
            {
                return Result<string>.Failure("Customer Not Found.");
            }

            var address = _mapper.Map<Address>(request);
            address.CreatedBy = userId.Value;

            await _addressRepository.AddAsync(address, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken); 

            return "Address Created Successfully."; 
        }
    }
}
