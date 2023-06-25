using Business.Constants;
using Entities.Models.Requests;
using FluentValidation;

namespace Business.ValidationRules;

public class CouponValidator : AbstractValidator<CouponRequest>
{
    public CouponValidator()
    {
        RuleFor(c => c.CouponCount).GreaterThan(0).WithMessage(CouponMessages.CouponCountGreaterThanZero);
        RuleFor(c => c.DiscountAmount).GreaterThan(0).WithMessage(CouponMessages.DiscountAmountGreaterThanZero);
        RuleFor(c => c.ValidDays).GreaterThan(0).WithMessage(CouponMessages.ValidDaysGreaterThanZero);
    }
}
