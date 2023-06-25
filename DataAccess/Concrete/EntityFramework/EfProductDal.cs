using Base.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using Entities.Concrete;
using Entities.Models.Responses;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework;

public class EfProductDal : EfGenericRepository<Product, SimpraProjectContext>, IProductDal
{
    public List<ProductResponse> GetAll(Expression<Func<ProductResponse, bool>>? filter = null)
    {
        using (SimpraProjectContext context = new SimpraProjectContext())
        {
            var result = from p in context.Products
                         join c in context.Categories on p.CategoryId equals c.Id
                         select new ProductResponse { Id = p.Id, Name = p.Name, Description = p.Description, CategoryName = c.Name, Price = p.Price, Stock = p.Stock, MaxPoint = p.MaxPoint, PointPercentage = p.PointPercentage };

            return filter != null ? result.Where(filter).ToList() : result.ToList();
        }
    }
}
