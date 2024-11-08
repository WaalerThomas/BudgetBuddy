using BudgetBuddy.Category.Model;
using BudgetBuddy.Category.Repositories;
using BudgetBuddy.Common.Service;
using BudgetBuddy.Contracts.Enums;

namespace BudgetBuddy.Data.Repositories;

public class CategoryRepository : Repository<CategoryDao>, ICategoryRepository
{
    public CategoryRepository(DatabaseContext context, ICurrentUserService currentUserService) : base(context, currentUserService)
    {
    }

    public CategoryDao Create(CategoryDao category)
    {
        // TODO: What happens when saving fails??
        
        category.ClientId = _currentUser.ClientId;
        category.CreatedAt = DateTime.UtcNow;
        
        var result = Context.Categories.Add(category);

        Context.SaveChanges();
        
        return result.Entity;
    }

    public Task<CategoryDao> CreateAsync(CategoryDao category)
    {
        throw new NotImplementedException();
    }

    public CategoryDao Update(CategoryDao category)
    {
        category.UpdatedAt = DateTime.UtcNow;
        
        var result = Context.Categories.Update(category);
        
        Context.SaveChanges();
        
        return result.Entity;
    }

    public Task<CategoryDao> UpdateAsync(CategoryDao category)
    {
        throw new NotImplementedException();
    }

    public void Delete(CategoryDao category)
    {
        throw new NotImplementedException();
    }

    public CategoryDao? GetById(int id)
    {
        return Context.Categories.FirstOrDefault(x => x.Id == id && x.ClientId == _currentUser.ClientId);
    }

    public IEnumerable<CategoryDao> GetByIds(IEnumerable<int> ids)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<CategoryDao> GetByType(CategoryType type)
    {
        return Context.Categories
            .Where(x => x.ClientId == _currentUser.ClientId && x.Type == type)
            .ToList();
    }

    public IEnumerable<CategoryDao> GetGroupsCategories(int groupId)
    {
        return Context.Categories
            .Where(x => x.ClientId == _currentUser.ClientId && x.GroupId == groupId)
            .ToList();
    }
}