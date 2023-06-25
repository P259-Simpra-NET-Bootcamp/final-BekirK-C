using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules;

public class ProductValidator : AbstractValidator<Product>
{
	public ProductValidator()
	{
        RuleFor(p => p.Name).MinimumLength(4).WithMessage(ProductMessages.InvalidName);
        RuleFor(p => p.Description).NotEmpty().WithMessage(ProductMessages.InvalidDescription);
        RuleFor(p => p.Price).GreaterThan(0).WithMessage(ProductMessages.InvalidPrice);
        RuleFor(p => p.Stock).GreaterThan(0).WithMessage(ProductMessages.InvalidStock);
    }
}
