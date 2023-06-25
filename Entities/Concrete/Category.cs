using Base.Entities.Abstract;

namespace Entities.Concrete;

public class Category : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime? CreatedAt { get; set; }
    public List<Product> Products { get; set; }
}
