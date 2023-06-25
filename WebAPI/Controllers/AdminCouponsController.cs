using Business.Abstract;
using Entities.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize(Roles = "admin")]
[Route("api/[controller]")]
[ApiController]
public class AdminCouponsController : ControllerBase
{
    private readonly ICouponService _couponManager;

    public AdminCouponsController(ICouponService couponService)
    {
        _couponManager = couponService;
    }

    /// <summary>
    /// Yeni bir kupon oluşturur.
    /// </summary>
    /// <remarks>
    /// Example Value: { "couponCount": 10, "discountAmount": 30, "validDays": 15 }  > 15 gün geçerli 30 TL'lik 10 tane kupon oluşturur.
    /// </remarks>
    /// <param name="couponRequest">Kupon oluşturma modeli</param>

    [HttpPost("generate-coupon")]
    public IActionResult GenerateCoupon(CouponRequest couponRequest)
    {
        var result = _couponManager.GenerateCoupon(couponRequest);
        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }

    /// <summary>
    /// Aktif tüm kuponları getirir.
    /// </summary>

    [HttpGet("active-coupons")]
    public IActionResult GetAllActiveCoupons()
    {
        var result = _couponManager.GetAllActiveCoupons();
        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }

    /// <summary>
    /// Kupon kodu ile veri tabanından kupon kontrolü yapılır
    /// </summary>
    /// <remarks>
    /// Example Value: code = AEF00434F5
    /// </remarks>
    /// <param name="code">Sorgulanmak istenen kupon kodu</param>

    [HttpGet("getcoupon-bycode")]
    public IActionResult GetCouponByCode(string code)
    {
        var result = _couponManager.GetCouponByCode(code);
        if (result.Success)
            return Ok(result);
        
        return NotFound(result);
    }

    /// <summary>
    /// Belirtilen kupon kodunun geçerli olup olmadığını kontrol eder.
    /// </summary>
    /// <remarks>
    /// Example Value: code = AEF00434F5
    /// </remarks>
    /// <param name="code">Sorgulanmak istenen kupon kodu</param>
    /// <returns></returns>

    [HttpGet("check-code-isvalid")]
    public IActionResult IsCouponValid(string code)
    {
        var result = _couponManager.IsCouponValid(code);
        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }

    /// <summary>
    /// Bir kuponu siler.
    /// </summary>
    /// <param name="couponId">Silinecek kupon Id değeri</param>
    /// <returns></returns>

    [HttpDelete("delete-coupons/{couponId}")]
    public IActionResult DeleteCoupon([FromRoute] int couponId)
    {
        var result = _couponManager.DeleteCoupon(couponId);
        if (result.Success)
            return Ok(result);
        
        return BadRequest(result);
    }
}
