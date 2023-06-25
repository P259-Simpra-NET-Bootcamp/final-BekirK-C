using Base.Entities.Abstract;
using System.Linq.Expressions;

namespace Base.DataAccess;

public interface IGenericRepository<T> where T : class, IEntity, new()
{
    List<T> GetAll(Expression<Func<T, bool>> filter = null);
    List<T> GetAllWithInclude(params string[] includes);
    T Get(Expression<Func<T, bool>> filter);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}
