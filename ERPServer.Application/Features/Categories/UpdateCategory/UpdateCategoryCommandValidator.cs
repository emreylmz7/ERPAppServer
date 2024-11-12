using FluentValidation;

namespace ERPServer.Application.Features.Categories.UpdateCategory;

public sealed class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        // Id: Required field for identifying the category to update
        RuleFor(p => p.Id)
            .NotEmpty().WithMessage("Category ID is required.");

        // Name: Should not exceed 100 characters if provided
        RuleFor(p => p.Name)
            .MaximumLength(100).WithMessage("Category name cannot exceed 100 characters.")
            .When(p => !string.IsNullOrEmpty(p.Name));

        // Description: Should not exceed 500 characters if provided
        RuleFor(p => p.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.")
            .When(p => !string.IsNullOrEmpty(p.Description));
    }
}
