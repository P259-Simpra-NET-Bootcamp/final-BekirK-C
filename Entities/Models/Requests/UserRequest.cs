namespace Entities.Models.Requests;

public class UserRequest
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ShippingAddress { get; set; }
}
