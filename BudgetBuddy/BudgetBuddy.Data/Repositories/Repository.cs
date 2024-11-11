using System.Linq.Expressions;
using BudgetBuddy.Common.Service;
using Microsoft.EntityFrameworkCore;

namespace BudgetBuddy.Data.Repositories;

public abstract class Repository<TEntity> where TEntity : class
{
    protected readonly DatabaseContext Context;
    protected readonly ICurrentUserService _currentUser;

    protected Repository(DatabaseContext context, ICurrentUserService currentUser)
    {
        Context = context;
        _currentUser = currentUser;
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
        return Context.Set<TEntity>().AsNoTracking().Where(predicate);
    }

    public virtual TEntity? Get(int id)
    {
        return Context.Set<TEntity>().Find(id);
    }

    public virtual IEnumerable<TEntity> GetAll()
    {
        return Context.Set<TEntity>().AsNoTracking().ToList();
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