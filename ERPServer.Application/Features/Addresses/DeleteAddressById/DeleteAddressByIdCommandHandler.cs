using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Addresses.DeleteAddressById
{
    internal sealed class DeleteAddressByIdCommandHandler : IRequestHandler<DeleteAddressByIdCommand, Result<string>>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAddressByIdCommandHandler(
            IAddressRepository addressRepository,
            IUnitOfWork unitOfWork)
        {
            _addressRepository = addressRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(DeleteAddressByIdCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the address by its ID
            var address = await _addressRepository.GetByExpressionAsync(a => a.Id == request.AddressId, cancellationToken);

            if (address == null)
            {
                // If address doesn't exist, return failure
                return Result<string>.Failure("Address Not Found.");
            }

            // Delete the address
            _addressRepository.Delete(address);

            // Save the changes to the database
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return "Address Deleted Successfully.";
        }
    }
}
