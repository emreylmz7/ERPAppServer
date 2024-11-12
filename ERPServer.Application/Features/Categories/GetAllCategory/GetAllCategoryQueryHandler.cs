using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Categories.GetAllCategory
{
    internal sealed class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, Result<List<Category>>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;

        public GetAllCategoryQueryHandler(
            ICategoryRepository categoryRepository,
            IUserRepository userRepository)
        {
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
        }

        public async Task<Result<List<Category>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var userId = await _userRepository.GetCurrentUserId();
            if (userId == null)
            {
                return Result<List<Category>>.Failure("User is not authenticated.");
            }

            List<Category> categories = await _categoryRepository
                .GetAll()
                .Where(c => c.CreatedBy == userId.Value)
                .OrderBy(c => c.Name)
                .ToListAsync(cancellationToken);

            return categories;
        }
    }
}
