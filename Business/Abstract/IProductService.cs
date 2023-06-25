using Base.Utilities.Results;
using Entities.Models.Requests;
using Entities.Models.Responses;

namespace Business.Abstract;

public interface IProductService
{
    IDataResult<List<ProductResponse>> GetAll();
    IDataResult<ProductResponse> GetById(int id);
    IDataResult<List<ProductResponse>> GetByCategoryId(int categoryId);
    IResult Add(ProductRequest productRequest);
    IResult Update(ProductRequest productRequest, int productId);
    IResult Delete(int id);
    IResult UpdateStock(int id, int quantity);
    IDataResult<List<ProductResponse>> GetProductsInStock();
    IDataResult<List<ProductResponse>> GetProductsAbovePrice(decimal price);
}
