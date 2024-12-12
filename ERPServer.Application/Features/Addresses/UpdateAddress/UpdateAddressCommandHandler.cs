using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;
using System.Threading.Tasks;
using System.Threading;

namespace ERPServer.Application.Features.Addresses.UpdateAddress
{
    internal sealed class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, Result<string>>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateAddressCommandHandler(
            IAddressRepository addressRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _addressRepository = addressRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<string>> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            // Fetch the existing address based on the ID provided
            var address = await _addressRepository.GetByExpressionAsync(a => a.Id == request.Id, cancellationToken);
            if (address == null)
            {
                return Result<string>.Failure("Address Not Found.");
            }

            // Map the updated data onto the existing address entity
            _mapper.Map(request, address);
            address.UpdatedDate = DateTime.Now;  // Set the updated date

            // Update the address in the repository
            _addressRepository.Update(address);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return "Address Updated Successfully.";
        }
    }
}
