using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private IProductService _productManager;

    public ProductsController(IProductService productManager)
    {
        _productManager = productManager;
    }

    /// <summary>
    /// Tüm ürünleri getirir.
    /// </summary>

    [HttpGet("getall-products")]
    public IActionResult GetAll()
    {
        var result = _productManager.GetAll();
        if (result.Success)
            return Ok(result);

        return BadRequest(result.Message);
    }

    /// <summary>
    /// Belirli bir kategoriye ait ürünleri getirir.
    /// </summary>
    /// <param name="id">İstenen kategori Id değeri</param>

    [HttpGet("get-products-bycategoryid")]
    public IActionResult GetProductsByCategoryId(int id)
    {
        var result = _productManager.GetByCategoryId(id);
        if (result.Success)
            return Ok(result);

        return BadRequest(result.Message);
    }

    /// <summary>
    /// Belirli bir ürünü Id'ye göre getirir.
    /// </summary>
    /// <param name="id">Ürün Id değeri</param>

    [HttpGet("get-product-byproductid/{id}")]
    public IActionResult GetById(int id)
    {
        var result = _productManager.GetById(id);
        if (result.Success)
            return Ok(result);

        return BadRequest(result.Message);
    }

    /// <summary>
    /// Stokta bulunnan ürünleri getirir.
    /// </summary>

    [HttpGet("get-products-instock")]
    public IActionResult GetProductsInStock()
    {
        var result = _productManager.GetProductsInStock();

        if (result.Success)
            return Ok(result);

        return BadRequest(result.Message);
    }

    /// <summary>
    /// Belirli bir fiyatın üstündeki ürünleri getirir.
    /// </summary>

    [HttpGet("get-products-above-price")]
    public IActionResult GetProductsAbovePrice([FromQuery] decimal price)
    {
        var result = _productManager.GetProductsAbovePrice(price);

        if (result.Success)
            return Ok(result);

        return BadRequest(result.Message);
    }
}
