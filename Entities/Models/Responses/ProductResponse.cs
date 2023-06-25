using Entities.Concrete;

namespace Entities.Models.Responses;

public class ProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string CategoryName { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int MaxPoint { get; set; }
    public int PointPercentage { get; set; }
}
