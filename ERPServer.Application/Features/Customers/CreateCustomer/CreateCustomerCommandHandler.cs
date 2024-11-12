using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Customers.CreateCustomer
{
    internal sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result<string>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public CreateCustomerCommandHandler(
            ICustomerRepository customerRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IUserRepository userRepository)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Result<string>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            // Sisteme giriş yapmış kullanıcının ID'sini al
            var userId = await _userRepository.GetCurrentUserId();
            if (userId == null)
            {
                return Result<string>.Failure("User is not authenticated.");
            }

            // Vergi numarasının zaten mevcut olup olmadığını kontrol et
            bool isTaxNumberExist = await _customerRepository.AnyAsync(p => p.TaxNumber == request.TaxNumber, cancellationToken);
            if (isTaxNumberExist)
            {
                return Result<string>.Failure("The tax number is already registered with another customer.");
            }

            // Customer'ı request'ten map et ve giriş yapan kullanıcının UserId'sini ekle
            Customer customer = _mapper.Map<Customer>(request);
            customer.UserId = userId.Value;  // Kullanıcının ID'sini Customer'a ekleyin

            // Müşteri kaydını veritabanına ekleyin
            await _customerRepository.AddAsync(customer, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return "Customer created successfully.";
        }
    }
}
