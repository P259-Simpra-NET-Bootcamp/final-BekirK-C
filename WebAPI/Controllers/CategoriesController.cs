using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private ICategoryService _categoryManager;

    public CategoriesController(ICategoryService categoryManager)
    {
        _categoryManager = categoryManager;
    }

    /// <summary>
    /// Tüm kategorileri getirir.
    /// </summary>

    [HttpGet("getall-categories")]
    public IActionResult GetAll()
    {
        var result = _categoryManager.GetAll();
        if (result.Success)
            return Ok(result);
        
        return BadRequest(result.Message);
    }

    /// <summary>
    /// Ürünleriyle birlikte tüm kategorileri getirir.
    /// </summary>

    [HttpGet("getall-categories-with-products")]
    public IActionResult GetAllWithInclude()
    {
        var result = _categoryManager.GetAllWithInclude();
        if (result.Success)
            return Ok(result);
        
        return BadRequest(result.Message);
    }
}
