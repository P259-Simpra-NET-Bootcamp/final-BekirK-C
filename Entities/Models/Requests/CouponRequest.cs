namespace Entities.Models.Requests;

public class CouponRequest
{
    public int CouponCount { get; set; }
    public int DiscountAmount { get; set; }
    public int ValidDays { get; set; }
}
