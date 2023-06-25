using Base.Utilities.Results;
using Business.Abstract;
using Business.Constants;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Models.Requests;
using FluentValidation;
using FluentValidation.Results;

namespace Business.Concrete;
public class CouponManager : ICouponService
{
    private readonly ICouponDal _couponDal;
    private readonly IValidator<CouponRequest> _couponValidator;

    public CouponManager(ICouponDal couponDal, IValidator<CouponRequest> couponValidator)
    {
        _couponDal = couponDal;
        _couponValidator = couponValidator;
    }

    public IResult GenerateCoupon(CouponRequest couponRequest)
    {
        ValidationResult result = _couponValidator.Validate(couponRequest);
        if (!result.IsValid)
            return new ErrorResult(result.Errors.FirstOrDefault()?.ErrorMessage);

        for (int i = 0; i < couponRequest.CouponCount; i++)
        {
            string uniqueCode = GenerateUniqueCode();

            Coupon coupon = new Coupon
            {
                Code = uniqueCode,
                IsActive = true,
                ExpirationDate = DateTime.Now.AddDays(couponRequest.ValidDays),
                CreatedAt = DateTime.Now,
                Discount = couponRequest.DiscountAmount
            };

            _couponDal.Add(coupon);
        }
        return new SuccessResult(CouponMessages.CouponsSuccessfullyGenerated);
    }

    public IDataResult<List<Coupon>> GetAllActiveCoupons()
    {
        var couponListResult = _couponDal.GetAll(c => c.IsActive && c.ExpirationDate >= DateTime.Now);
        if (!couponListResult.Any())
            return new ErrorDataResult<List<Coupon>>(CouponMessages.NoActiveCouponsFound);

        return new SuccessDataResult<List<Coupon>>(couponListResult, CouponMessages.ActiveCouponsListed);
    }

    public IDataResult<Coupon> GetCouponByCode(string code)
    {
        var coupon = _couponDal.Get(c => c.Code == code);
        if (coupon == null)
            return new ErrorDataResult<Coupon>(CouponMessages.CouponNotFound);

        return new SuccessDataResult<Coupon>(coupon, CouponMessages.CouponListed);
    }

    public IResult IsCouponValid(string code)
    {
        var coupon = GetCouponByCode(code);
        if (!coupon.Success || coupon.Data == null || !coupon.Data.IsActive || coupon.Data.ExpirationDate < DateTime.Now)
            return new ErrorResult(CouponMessages.InvalidCouponCode);

        return new SuccessResult(CouponMessages.ValidCoupon);
    }

    public IResult UpdateCoupon(Coupon coupon)
    {
        var isCouponExist = _couponDal.Get(c => c.Id == coupon.Id);
        if (isCouponExist == null)
        {
            return new ErrorResult(CouponMessages.CouponNotFound);
        }
        _couponDal.Update(coupon);
        return new SuccessResult(CouponMessages.CouponUpdated);
    }

    public IResult DeleteCoupon(int couponId)
    {
        var coupon = _couponDal.Get(c => c.Id == couponId);
        if (coupon == null)
        {
            return new ErrorResult(CouponMessages.CouponNotFound);
        }
        _couponDal.Delete(coupon);
        return new SuccessResult(CouponMessages.CouponDeleted);
    }

    private string GenerateUniqueCode()
    {
        Guid guid = Guid.NewGuid();
        string couponCode = guid.ToString().Replace("-", "").Substring(0, 10).ToUpper();
        return couponCode;
    }

    public decimal ApplyCouponDiscount(decimal totalPrice, string couponCode)
    {
        if (string.IsNullOrWhiteSpace(couponCode))
            return totalPrice;

        var coupon = _couponDal.Get(c => c.Code == couponCode && c.IsActive && c.ExpirationDate >= DateTime.Now);

        if (coupon == null)
            return -1;

        decimal discountAmount = coupon.Discount;
        decimal discountedPrice = totalPrice - discountAmount;

        return discountedPrice < 0 ? 0 : discountedPrice;
    }
}
