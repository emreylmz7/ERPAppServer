using FluentValidation;

namespace ERPServer.Application.Features.Categories.CreateCategory
{
    public sealed class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            // Name: Should not be empty or null and have a maximum length
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Category name cannot exceed 100 characters.");

            // Description: Should not exceed a maximum length
            RuleFor(c => c.Description)
                .MaximumLength(500).WithMessage("Category description cannot exceed 500 characters.");
        }
    }
}
