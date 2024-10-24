using System.Linq.Expressions;
using BudgetBuddy.Common.Database;

namespace BudgetBuddy.Common.Repositories;

public abstract class Repository<TEntity> where TEntity : class
{
    protected readonly DatabaseContext Context;

    protected Repository(DatabaseContext context)
    {
        Context = context;
    }

    public virtual void Add(TEntity entity)
    {
        Context.Set<TEntity>().Add(entity);
    }

    public virtual void AddRange(IEnumerable<TEntity> entities)
    {
        Context.Set<TEntity>().AddRange(entities);
    }

    public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    {
        return Context.Set<TEntity>().Where(predicate);
    }

    public virtual TEntity? Get(int id)
    {
        return Context.Set<TEntity>().Find(id);
    }

    public virtual IEnumerable<TEntity> GetAll()
    {
        return Context.Set<TEntity>().ToList();
    }

    public virtual void Remove(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<TEntity> entities)
    {
        Context.Set<TEntity>().RemoveRange(entities);
    }
}