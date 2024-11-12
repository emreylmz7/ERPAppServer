using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Customers.GetAllCustomer
{
    internal sealed class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQuery, Result<List<Customer>>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRepository _userRepository;

        public GetAllCustomerQueryHandler(
            ICustomerRepository customerRepository,
            IUserRepository userRepository)
        {
            _customerRepository = customerRepository;
            _userRepository = userRepository;
        }

        public async Task<Result<List<Customer>>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
        {
            var userId = await _userRepository.GetCurrentUserId();
            if (userId == null)
            {
                return Result<List<Customer>>.Failure("User is not authenticated.");
            }

            // Kullanıcının ID'sine sahip müşterileri sorgula
            List<Customer> customers = await _customerRepository
                .GetAll()
                .Where(c => c.UserId == userId.Value)  
                .OrderBy(p => p.FirstName)
                .ToListAsync(cancellationToken);

            return customers;
        }
    }
}
