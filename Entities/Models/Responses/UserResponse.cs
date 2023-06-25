namespace Entities.Models.Responses;

public class UserResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public decimal VirtualWallet { get; set; }
    public decimal EarnedPoints { get; set; }
    public string Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string ShippingAddress { get; set; }
}
