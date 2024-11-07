﻿using BudgetBuddy.Common.Database;
using BudgetBuddy.Common.Repositories;
using BudgetBuddy.Common.Service;
using BudgetBuddy.Contracts.Model.Category;

namespace BudgetBuddy.Category.Repositories;

public class CategoryRepository : Repository<CategoryModel>, ICategoryRepository
{
    public CategoryRepository(DatabaseContext context, ICurrentUserService currentUserService) : base(context, currentUserService)
    {
    }

    public CategoryModel Create(CategoryModel model)
    {
        // TODO: What happens when saving fails??
        
        model.ClientId = _currentUser.ClientId;
        model.CreatedAt = DateTime.UtcNow;
        
        var result = Context.Categories.Add(model);

        Context.SaveChanges();
        
        return result.Entity;
    }

    public Task<CategoryModel> CreateAsync(CategoryModel model)
    {
        throw new NotImplementedException();
    }

    public CategoryModel Update(CategoryModel model)
    {
        throw new NotImplementedException();
    }

    public Task<CategoryModel> UpdateAsync(CategoryModel model)
    {
        throw new NotImplementedException();
    }

    public void Delete(CategoryModel model)
    {
        throw new NotImplementedException();
    }

    public CategoryModel? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<CategoryModel> GetByIds(IEnumerable<int> ids)
    {
        throw new NotImplementedException();
    }
}