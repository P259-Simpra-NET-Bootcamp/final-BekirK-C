using Business.Constants;
using Entities.Models.Requests;
using FluentValidation;

namespace Business.ValidationRules;

public class UserRegisterValidator : AbstractValidator<UserRegisterRequest>
{
    public UserRegisterValidator()
    {
        RuleFor(u => u.Email).NotEmpty().EmailAddress().WithMessage(UserMessages.ValidEmailRequired);
        RuleFor(u => u.Password).NotEmpty().MinimumLength(3).WithMessage(UserMessages.ValidPasswordLength);
        RuleFor(u => u.FirstName).NotEmpty().WithMessage(UserMessages.FirstNameRequired);
        RuleFor(u => u.LastName).NotEmpty().WithMessage(UserMessages.LastNameRequired);
        RuleFor(u => u.ShippingAddress).NotEmpty().WithMessage(UserMessages.ShippingAddressRequired);
    }
}
