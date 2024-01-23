using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Experiment.Infrastructure.Repositories;

public interface IRepository<T> where T : class
{
    T? Get(int id);
    IQueryable<T> GetAll(Expression<Func<T, bool>>? filter = null);
    IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}


public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly DbContext Context;

    public Repository(DbContext context)
    {
        Context = context;
    }

    public TEntity? Get(int id)
    {
        return Context.Set<TEntity>().Find(id);
    }

    public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter = null)
    {
        if (filter != null)
        {
            return Context.Set<TEntity>().Where(filter);
        }
        
        return Context.Set<TEntity>();
    }

    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    {
        return Context.Set<TEntity>().Where(predicate);
    }

    public void Add(TEntity entity)
    {
        Context.Set<TEntity>().Add(entity);
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
        Context.Set<TEntity>().AddRange(entities);
    }

    public void Remove(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        Context.Set<TEntity>().RemoveRange(entities);
    }
}

