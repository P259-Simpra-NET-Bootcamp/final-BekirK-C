using Base.Entities.Abstract;

namespace Base.Entities.Concrete;

public class User : IEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public decimal VirtualWallet { get; set; }
    public decimal EarnedPoints { get; set; }
    public string Status { get; set; }
    public string ShippingAddress { get; set; }
    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; }
    public DateTime? CreatedAt { get; set; }
}
