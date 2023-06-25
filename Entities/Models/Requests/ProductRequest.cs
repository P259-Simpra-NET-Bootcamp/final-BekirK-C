namespace Entities.Models.Requests;

public class ProductRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int MaxPoint { get; set; }
    public int PointPercentage { get; set; }
}
