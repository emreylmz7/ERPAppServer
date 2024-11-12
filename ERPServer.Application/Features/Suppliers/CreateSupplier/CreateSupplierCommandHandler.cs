using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Suppliers.CreateSupplier
{
    internal sealed class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, Result<string>>
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public CreateSupplierCommandHandler(
            ISupplierRepository supplierRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IUserRepository userRepository)
        {
            _supplierRepository = supplierRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Result<string>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            var userId = await _userRepository.GetCurrentUserId();
            if (userId == null)
            {
                return Result<string>.Failure("User is not authenticated.");
            }

            bool isSupplierExist = await _supplierRepository.AnyAsync(s => s.CompanyName == request.CompanyName, cancellationToken);
            if (isSupplierExist)
            {
                return Result<string>.Failure("A supplier with this company name already exists.");
            }

            Domain.Entities.Supplier supplier = _mapper.Map<Domain.Entities.Supplier>(request);
            supplier.CreatedBy = userId.Value;

            await _supplierRepository.AddAsync(supplier, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return "Supplier created successfully.";
        }
    }
}
