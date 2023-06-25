using Business.Abstract;
using Entities.Models.Requests;
using Entities.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminAuthController : ControllerBase
{
    private IAuthService _authManager;
    private IUserService _userManager;

    public AdminAuthController(IAuthService authService, IUserService userManager)
    {
        _authManager = authService;
        _userManager = userManager;
    }

    /// <summary>
    /// Admin kayıt işlemi yapılır. (Yalnızca admin kullanıcı admin kayıt işlemini gerçekleştirebilir.)
    /// </summary>
    /// <remarks>
    /// Example Value: { "email": "bekir@admin.com", "password": "12345", "firstName": "Bekir", "lastName": "Kamacı", "shippingAddress": "Ümraniye/İstanbul" }
    /// </remarks>
    /// <param name="userRegisterRequest">Admin kullanıcı kayıt modeli</param>

    [Authorize(Roles = "admin")]
    [HttpPost("admin-register")]
    public ActionResult AdminRegister(UserRegisterRequest userRegisterRequest)
    {
        var userExists = _authManager.UserExists(userRegisterRequest.Email);
        if (!userExists.Success)
            return BadRequest(userExists.Message);

        var registerResult = _authManager.Register(userRegisterRequest, isAdmin: true);
        if (!registerResult.Success)
            return BadRequest(registerResult.Message);

        var result = _authManager.CreateAccessToken(registerResult.Data);
        if (result.Success)
            return Ok(result.Data);

        return BadRequest(result.Message);
    }

    /// <summary>
    /// Admin girişi işlemi yapılır.
    /// </summary>
    /// <remarks>
    /// Example Value: { "email": "bekir@admin.com", "password": "12345" }
    /// </remarks>
    /// <param name="userLoginRequest">Admin kullanıcı giriş modeli</param>

    [HttpPost("admin-login")]
    public ActionResult Login(UserLoginRequest userLoginRequest)
    {
        var userToLogin = _authManager.Login(userLoginRequest);
        if (!userToLogin.Success)
            return BadRequest(userToLogin.Message);

        var result = _authManager.CreateAccessToken(userToLogin.Data);
        if (result.Success)
            return Ok(result);

        return BadRequest(result.Message);
    }

    /// <summary>
    /// Tüm kullanıcıları getirir.
    /// </summary>

    [Authorize(Roles = "admin")]
    [HttpGet("getall-users")]
    public ActionResult<List<UserResponse>> GetAll()
    {
        var result = _userManager.GetAll();
        if (result.Success)
            return Ok(result.Data);

        return BadRequest(result.Message);
    }

    /// <summary>
    /// Kullanıcı güncelleme işlemi yapılır
    /// </summary>
    /// <remarks>
    /// Example Value:{ "id": 0, "firstName": "string", "lastName": "string", "shippingAddress": "string" }
    /// </remarks>
    /// <param name="userRequest">Kullanıcı güncelleme modeli</param>

    [Authorize(Roles = "admin")]
    [HttpPut("update-customer")]
    public ActionResult Update(UserRequest userRequest)
    {
        var result = _userManager.Update(userRequest);
        if (result.Success)
            return Ok();

        return BadRequest(result.Message);
    }

    /// <summary>
    /// Müşteri silme işlemi gerçekleştirilir.
    /// </summary>
    /// <param name="id">Silinecek kullanıcının Id değeri</param>
    /// <returns></returns>

    [Authorize(Roles = "admin")]
    [HttpDelete("delete-customer/{id}")]
    public ActionResult Delete([FromRoute] int id)
    {
        var result = _userManager.Delete(id);
        if (result.Success)
            return Ok();

        return BadRequest(result.Message);
    }
}
