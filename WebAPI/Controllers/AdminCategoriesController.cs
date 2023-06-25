using Business.Abstract;
using Entities.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize(Roles = "admin")]
[Route("api/[controller]")]
[ApiController]
public class AdminCategoriesController : ControllerBase
{
    private ICategoryService _categoryManager;

    public AdminCategoriesController(ICategoryService categoryManager)
    {
        _categoryManager = categoryManager;
    }

    /// <summary>
    /// Yeni bir kategori ekler.
    /// </summary>
    /// <remarks>
    /// Example Value: { "name": "Category 5", "description": "Description of Category 5"}
    /// </remarks>
    /// <param name="categoryRequest">Kategori ekleme modeli</param>

    [HttpPost("add-category")]
    public IActionResult Add([FromBody] CategoryRequest categoryRequest)
    {
        var result = _categoryManager.Add(categoryRequest);
        if (result.Success)
            return Ok(result);
        
        return BadRequest(result.Message);
    }

    /// <summary>
    /// Bir kategoriyi günceller.
    /// </summary>
    /// <remarks>
    /// Example Value: { "name": "Category 5", "description": "Description of Category 5"} /categoryId = 1
    /// </remarks>
    /// <param name="categoryRequest">Kategori güncelleme modeli</param>

    [HttpPut("update-category/{categoryId}")]
    public IActionResult Update([FromBody] CategoryRequest categoryRequest, [FromRoute] int categoryId)
    {
        var result = _categoryManager.Update(categoryRequest, categoryId);
        if (result.Success)
            return Ok(result);
        
        return BadRequest(result.Message);
    }

    /// <summary>
    /// Bir kategoriyi siler.
    /// </summary>
    /// <param name="categoryId">Silinecek kategorinin Id değeri</param>
    /// <returns></returns>

    [HttpDelete("delete-category/{categoryId}")]
    public IActionResult Delete([FromRoute] int categoryId)
    {
        var result = _categoryManager.Delete(categoryId);
        if (result.Success)
            return Ok(result);
        
        return BadRequest(result.Message);
    }
}
