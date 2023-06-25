using Business.Abstract;
using Entities.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private IAuthService _authManager;

    public AuthController(IAuthService authService)
    {
        _authManager = authService;
    }

    /// <summary>
    /// Müşteri girişi yapılır
    /// </summary>
    /// <param name="uerLoginRequest">Giriş isteği bilgileri</param>

    [HttpPost("customer-login")]
    public ActionResult Login(UserLoginRequest uerLoginRequest)
    {
        var userToLogin = _authManager.Login(uerLoginRequest);
        if (!userToLogin.Success)
            return BadRequest(userToLogin.Message);

        var result = _authManager.CreateAccessToken(userToLogin.Data);
        if (result.Success)
            return Ok(result);

        return BadRequest(result.Message);
    }

    /// <summary>
    /// Müşteri kaydı yapılır.
    /// </summary>
    /// <param name="userRegisterRequest">Kayıt isteği bilgileri</param>

    [HttpPost("customer-register")]
    public ActionResult Register(UserRegisterRequest userRegisterRequest)
    {
        var userExists = _authManager.UserExists(userRegisterRequest.Email);
        if (!userExists.Success)
            return BadRequest(userExists.Message);

        var registerResult = _authManager.Register(userRegisterRequest);
        if (!registerResult.Success)
            return BadRequest(registerResult.Message);

        var result = _authManager.CreateAccessToken(registerResult.Data);
        if (result.Success)
            return Ok(result.Data);

        return BadRequest(result.Message);
    }

    /// <summary>
    /// Müşteri bilgilerini günceller.
    /// </summary>
    /// <param name="userRegisterRequest">Güncelleme isteği bilgileri</param>

    [Authorize(Roles = "customer")]
    [HttpPost("customer-update")]
    public ActionResult Update(UserRegisterRequest userRegisterRequest)
    {
        var registerResult = _authManager.Update(userRegisterRequest);
        if (registerResult.Success)
            return Ok(registerResult.Message);

        return BadRequest(registerResult.Message);
    }
}
