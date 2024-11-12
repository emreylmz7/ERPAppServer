using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Suppliers.GetAllSupplier
{
    internal sealed class GetAllSupplierQueryHandler : IRequestHandler<GetAllSupplierQuery, Result<List<Supplier>>>
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IUserRepository _userRepository;

        public GetAllSupplierQueryHandler(
            ISupplierRepository supplierRepository,
            IUserRepository userRepository)
        {
            _supplierRepository = supplierRepository;
            _userRepository = userRepository;
        }

        public async Task<Result<List<Supplier>>> Handle(GetAllSupplierQuery request, CancellationToken cancellationToken)
        {
            var userId = await _userRepository.GetCurrentUserId();
            if (userId == null)
            {
                return Result<List<Supplier>>.Failure("User is not authenticated.");
            }

            List<Supplier> suppliers = await _supplierRepository
                .GetAll()
                .Where(s => s.CreatedBy == userId.Value)
                .OrderBy(s => s.CompanyName)
                .ToListAsync(cancellationToken);

            return suppliers.Any()
                ? suppliers
                : Result<List<Supplier>>.Failure("No suppliers found.");
        }
    }
}
