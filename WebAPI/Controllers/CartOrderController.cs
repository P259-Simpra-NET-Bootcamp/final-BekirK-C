using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize(Roles = "admin,customer")]
[Route("api/[controller]")]
[ApiController]
public class CartOrderController : ControllerBase
{
    private readonly ICartOrderService _cartOrderManager;

    public CartOrderController(ICartOrderService cartOrderManager)
    {
        _cartOrderManager = cartOrderManager;
    }

    /// <summary>
    /// Sepette bulunan ürünlerin siparişini verir. (Kullanıcının sanal cüzdanından ödeme yapılmaktadır. Eğer sanal cüzdanda para yoksa ""add-money-towallet" isteği ile para yüklenmesi gerekmektedir.)
    /// </summary>
    /// <param name="couponCode"></param>

    [HttpPost("place-order")]
    public IActionResult PlaceCartOrder([FromQuery] string? couponCode)
    {
        var result = _cartOrderManager.PlaceCartOrder(couponCode);
        if (result.Success)
            return Ok(result.Message);

        return BadRequest(result.Message);
    }

    /// <summary>
    /// Sipariş geçmişini ve detaylarını getirir.
    /// </summary>

    [HttpGet("get-order-details")]
    public IActionResult GetOrderDetails()
    {
        var result = _cartOrderManager.GetOrderDetails();

        if (result.Success)
            return Ok(result.Data);

        return BadRequest(result.Message);
    }
}
