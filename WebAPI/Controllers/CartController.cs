using Business.Abstract;
using Entities.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize(Roles = "admin,customer")]
[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartService _cartManager;

    public CartController(ICartService cartService)
    {
        _cartManager = cartService;
    }

    /// <summary>
    /// Sepete ürün ekler.
    /// </summary>
    /// <remarks>
    /// { "productId": 1, "quantity": 2 }
    /// </remarks>
    /// <param name="cartItemRequest">Sepete eklenecek ürün bilgileri</param>

    [HttpPost("add-to-cart")]
    public IActionResult AddToCart(CartItemRequest cartItemRequest)
    {
        var result = _cartManager.AddToCart(cartItemRequest);
        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }

    /// <summary>
    /// Sepetteki ürünleri getirir.
    /// </summary>

    [HttpGet("get-cart-details")]
    public IActionResult GetCartDetails()
    {
        var result = _cartManager.GetCartDetails();

        if (result.Success)
            return Ok(result.Data);

        return BadRequest(result.Message);
    }

    /// <summary>
    /// Sepetteki ürünün miktarını azaltır.
    /// </summary>
    /// <remarks>
    /// { "productId": 1, "quantity": 2 }
    /// </remarks>
    /// <param name="cartItemRequest">Sepetten azaltılacak ürün bilgileri</param>

    [HttpPut("decrement-product-incart")]
    public IActionResult DecrementQuantityInCart(CartItemRequest cartItemRequest)
    {
        var result = _cartManager.DecrementQuantityInCart(cartItemRequest);
        if (result.Success)
            return Ok(result.Message);

        return BadRequest(result.Message);
    }

    /// <summary>
    /// Sepetteki istenen ürünü çıkarır.
    /// </summary>
    /// <param name="productId">Çıkarılacak ürünün Id değeri</param>

    [HttpDelete("remove-product-incart/{productId}")]
    public IActionResult RemoveProductFromCart([FromRoute] int productId)
    {
        var result = _cartManager.RemoveProductFromCart(productId);
        if (result.Success)
            return Ok(result.Message);


        return BadRequest(result.Message);
    }

    /// <summary>
    /// Sepetteki tüm ürünleri çıkarır.
    /// </summary>

    [HttpDelete("remove-all-products-incart")]
    public IActionResult RemoveAllFromCart()
    {
        var result = _cartManager.RemoveAllFromCart();

        if (result.Success)
            return Ok(result.Message);

        return BadRequest(result.Message);
    }
}
