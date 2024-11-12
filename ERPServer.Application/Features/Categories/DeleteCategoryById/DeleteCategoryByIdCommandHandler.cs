using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Categories.DeleteCategoryById;

internal sealed class DeleteCategoryByIdCommandHandler : IRequestHandler<DeleteCategoryByIdCommand, Result<string>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryByIdCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteCategoryByIdCommand request, CancellationToken cancellationToken)
    {
        // Attempt to retrieve the category by its ID
        Domain.Entities.Category category = await _categoryRepository.GetByExpressionAsync(c => c.Id == request.Id, cancellationToken);
        if (category is null)
        {
            return Result<string>.Failure("Category not found");
        }

        // Delete the category if it exists
        _categoryRepository.Delete(category);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return "Category deleted successfully";
    }
}
