using Base.Entities.Abstract;

namespace Entities.Concrete;

public class Product : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int MaxPoint { get; set; }
    public int PointPercentage { get; set; }
    public DateTime? CreatedAt { get; set; }
    public Category Category { get; set; }
}
