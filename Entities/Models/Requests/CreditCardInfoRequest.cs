namespace Entities.Models.Requests;

public class CreditCardInfoRequest
{
    public string CardNumber { get; set; }
    public string CardHolderName { get; set; }
    public string ExpirationMonth { get; set; }
    public string ExpirationYear { get; set; }
    public string CVV { get; set; }
}
