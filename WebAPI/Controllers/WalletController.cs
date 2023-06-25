using Business.Abstract;
using Entities.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize(Roles = "admin,customer")]
[Route("api/[controller]")]
[ApiController]
public class WalletController : ControllerBase
{
    private readonly IWalletService _walletManager;

    public WalletController(IWalletService walletManager)
    {
        _walletManager = walletManager;
    }

    /// <summary>
    /// Sanal cüzdana para ekleme işlemi yapar. (Kart Numarası 16 haneli, ay 2 haneli ve yıl 4 haneli olmak zorundadır)
    /// </summary>
    /// <remarks>
    /// Example Value: { "cardNumber": "1234567890123456", "cardHolderName": "Bekir Kamacı", "expirationMonth": "04", "expirationYear": "2024", "cvv": "122" }
    /// </remarks>
    /// <param name="creditCardInfoRequest">Kredi kartı bilgileri</param>
    /// <param name="amount">Eklenecek para miktarı</param>

    [HttpPost("add-money-towallet")]
    public IActionResult AddMoneyToWallet([FromBody] CreditCardInfoRequest creditCardInfoRequest, [FromQuery] decimal amount)
    {
        var result = _walletManager.AddMoneyToWallet(creditCardInfoRequest, amount);

        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }

    /// <summary>
    /// Cüzdan bakiyesini kontrol eder.
    /// </summary>

    [HttpGet("check-wallet-balance")]
    public IActionResult GetWalletBalance()
    {
        var result = _walletManager.GetWalletBalance();

        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }
}