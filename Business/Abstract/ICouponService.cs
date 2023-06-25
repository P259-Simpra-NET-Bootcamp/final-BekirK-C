using Base.Utilities.Results;
using Entities.Concrete;
using Entities.Models.Requests;

namespace Business.Abstract;

public interface ICouponService
{
    IResult GenerateCoupon(CouponRequest couponRequest);
    IDataResult<List<Coupon>> GetAllActiveCoupons();
    IDataResult<Coupon> GetCouponByCode(string code);
    IResult UpdateCoupon(Coupon coupon);
    IResult IsCouponValid(string code);
    IResult DeleteCoupon(int couponId);
    decimal ApplyCouponDiscount(decimal totalPrice, string couponCode);
}
