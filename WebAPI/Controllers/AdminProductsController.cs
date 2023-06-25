using Business.Abstract;
using Entities.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize(Roles = "admin")]
[Route("api/[controller]")]
[ApiController]
public class AdminProductsController : ControllerBase
{
    private IProductService _productManager;

    public AdminProductsController(IProductService productManager)
    {
        _productManager = productManager;
    }

    /// <summary>
    /// Yeni bir ürün ekler.
    /// </summary>
    /// <remarks>
    /// { "name": "Product 5", "description": "Description of Product 5", "categoryId": 1, "price": 45, "stock": 500, "maxPoint": 20, "pointPercentage": 15 }
    /// </remarks>
    /// <param name="productRequest">Ürün ekleme modeli</param>

    [HttpPost("add-product")]
    public IActionResult Add([FromBody] ProductRequest productRequest)
    {
        var result = _productManager.Add(productRequest);
        if (result.Success)
            return Ok(result);

        return BadRequest(result.Message);
    }

    /// <summary>
    /// Ürün bilgilerini günceller.
    /// </summary>
    /// <remarks>
    /// { "name": "Product 5", "description": "Description of Product 5", "categoryId": 1, "price": 45, "stock": 500, "maxPoint": 20, "pointPercentage": 15 } /productId = 5
    /// </remarks>
    /// <param name="productRequest">Güncellenmek istenen ürün modeli</param>
    /// <param name="productId">Güncellenmek istenen ürün Id değeri </param>

    [HttpPut("update-product/{productId}")]
    public IActionResult Update([FromBody] ProductRequest productRequest, [FromRoute] int productId)
    {
        var result = _productManager.Update(productRequest, productId);
        if (result.Success)
            return Ok(result);

        return BadRequest(result.Message);
    }

    /// <summary>
    /// Ürünün stok miktarını azaltır.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="quantity"></param>

    [HttpPut("{id}/product-decrease-stock")]
    public IActionResult DecreaseStock([FromRoute] int id, [FromBody] int quantity)
    {
        var result = _productManager.UpdateStock(id, -quantity);
        if (result.Success)
            return Ok(result);

        return BadRequest(result.Message);
    }

    /// <summary>
    /// Ürünün stok miktarını artırır.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="quantity"></param>

    [HttpPut("{id}/product-increase-stock")]
    public IActionResult IncreaseStock([FromRoute] int id, [FromBody] int quantity)
    {
        var result = _productManager.UpdateStock(id, quantity);
        if (result.Success)
            return Ok(result);

        return BadRequest(result.Message);
    }

    /// <summary>
    /// Bir ürünü siler.
    /// </summary>
    /// <param name="productId"></param>

    [HttpDelete("delete-product/{productId}")]
    public IActionResult Delete([FromRoute] int productId)
    {
        var result = _productManager.Delete(productId);
        if (result.Success)
            return Ok(result);

        return BadRequest(result.Message);
    }
}
