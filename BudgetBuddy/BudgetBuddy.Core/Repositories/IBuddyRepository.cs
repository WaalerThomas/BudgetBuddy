using System.Linq.Expressions;
using BudgetBuddy.Core.Database;

namespace BudgetBuddy.Core.Repository;

public interface IBuddyRepository<T> where T : BuddyDto
{
    /// <summary>
    /// Creates a new database entry
    /// </summary>
    /// <param name="model">Entity model to be created</param>
    /// <returns>Created entity with ID set</returns>
    T Create(T model);
    Task<T> CreateAsync(T model);
    
    /// <summary>
    /// Updates existing database entity
    /// </summary>
    /// <param name="model">Entity model to be updated</param>
    /// <returns>Updated model</returns>
    T Update(T model);
    Task<T> UpdateAsync(T model);
    
    /// <summary>
    /// Deletes existing database entity
    /// </summary>
    /// <param name="model">Entity model to be deleted</param>
    void Delete(T model);
    
    /// <summary>
    /// Get entity by primary key
    /// </summary>
    /// <param name="id">Primary key</param>
    /// <returns>Retrieved entity from database</returns>
    T GetById(int id);
    IEnumerable<T> GetAll();
    IEnumerable<T> GetByIds(IEnumerable<int> ids);
}