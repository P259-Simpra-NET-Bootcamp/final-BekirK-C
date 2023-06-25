using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules;

public class CategoryValidator : AbstractValidator<Category>
{
    public CategoryValidator()
    {
        RuleFor(p => p.Name).MinimumLength(4).WithMessage(CategoryMessages.InvalidName);
        RuleFor(p => p.Description).NotEmpty().WithMessage(CategoryMessages.InvalidDescription);
    }
}
