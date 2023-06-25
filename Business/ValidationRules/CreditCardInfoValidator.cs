using Business.Constants;
using Entities.Models.Requests;
using FluentValidation;

namespace Business.ValidationRules;

public class CreditCardInfoValidator : AbstractValidator<CreditCardInfoRequest>
{
    public CreditCardInfoValidator()
    {
        RuleFor(request => request.CardNumber)
            .NotEmpty().WithMessage(WalletMessages.CardNumberRequired)
            .Length(16).WithMessage(WalletMessages.ValidCardNumberLength);

        RuleFor(request => request.CardHolderName)
            .NotEmpty().WithMessage(WalletMessages.CardHolderNameRequired);

        RuleFor(request => request.ExpirationMonth)
            .NotEmpty().WithMessage(WalletMessages.ExpirationMonthRequired)
            .Matches(@"^(0[1-9]|1[0-2])$").WithMessage(WalletMessages.InvalidExpirationMonth);

        RuleFor(request => request.ExpirationYear)
            .NotEmpty().WithMessage(WalletMessages.ExpirationYearRequired)
            .Matches(@"^(20)\d{2}$").WithMessage(WalletMessages.InvalidExpirationYear);

        RuleFor(request => request.CVV)
            .NotEmpty().WithMessage(WalletMessages.CVVRequired)
            .Matches(@"^\d{3,4}$").WithMessage(WalletMessages.InvalidCVV);
    }
}
