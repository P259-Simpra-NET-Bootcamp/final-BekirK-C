using Base.Entities.Abstract;

namespace Entities.Concrete;

public class CartItem : IEntity
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime? CreatedAt { get; set; }
}
