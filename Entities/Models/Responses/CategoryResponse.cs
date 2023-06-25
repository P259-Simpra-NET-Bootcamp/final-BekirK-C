namespace Entities.Models.Responses;

public class CategoryResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<ProductResponse> Products { get; set; }
}
