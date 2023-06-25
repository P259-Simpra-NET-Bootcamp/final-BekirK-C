using Base.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Base.DataAccess.EntityFramework;

public class EfGenericRepository<TEntity, TContext> : IGenericRepository<TEntity> where TEntity : class, IEntity, new() where TContext : DbContext, new()
{
    public void Add(TEntity entity)
    {
        using (TContext context = new TContext())
        {
            var addedEntity = context.Entry(entity);
            addedEntity.State = EntityState.Added;
            context.SaveChanges();
        }
    }

    public void Delete(TEntity entity)
    {
        using (TContext context = new TContext())
        {
            var deleteEntity = context.Entry(entity);
            deleteEntity.State = EntityState.Deleted;
            context.SaveChanges();
        }
    }

    public TEntity Get(Expression<Func<TEntity, bool>> filter)
    {
        using (TContext context = new TContext())
        {
            return context.Set<TEntity>().SingleOrDefault(filter);
        }
    }

    public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
    {
        using (TContext context = new TContext())
        {
            return filter == null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(filter).ToList();
        }
    }

    public List<TEntity> GetAllWithInclude(params string[] includes)
    {
        using (var context = new TContext())
        {
            var query = context.Set<TEntity>().AsQueryable();
            query = includes.Aggregate(query, (current, inc) => current.Include(inc));
            return query.ToList();
        }
    }

    public void Update(TEntity entity)
    {
        using (TContext context = new TContext())
        {
            var updatedEntity = context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
