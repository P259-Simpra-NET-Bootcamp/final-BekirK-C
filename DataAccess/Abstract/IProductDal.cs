using Base.DataAccess;
using Entities.Concrete;
using Entities.Models.Responses;
using System.Linq.Expressions;

namespace DataAccess.Abstract;

public interface IProductDal : IGenericRepository<Product>
{
    List<ProductResponse> GetAll(Expression<Func<ProductResponse, bool>>? filter = null);
}
