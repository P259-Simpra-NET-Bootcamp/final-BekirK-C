using Base.Entities.Abstract;

namespace Entities.Concrete;

public class Order : IEntity
{
    public int Id { get; set; }
    public string OrderId { get; set; }
    public int ProductId { get; set; }
    public int CustomerId { get; set; }
    public string? CouponCode { get; set; }
    public int? CouponAmount { get; set; }
    public decimal? SpendPoint { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public string ShippingAddress { get; set; }
    public DateTime OrderDate { get; set; }
}
