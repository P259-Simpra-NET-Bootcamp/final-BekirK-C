using Base.Entities.Abstract;

namespace Entities.Concrete;

public class Coupon : IEntity
{
    public int Id { get; set; }
    public string Code { get; set; }
    public int Discount { get; set; }
    public bool IsActive { get; set; }
    public DateTime ExpirationDate { get; set; }
    public DateTime CreatedAt { get; set; }
}
