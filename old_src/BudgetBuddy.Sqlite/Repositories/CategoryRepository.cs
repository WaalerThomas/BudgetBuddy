using BudgetBuddy.Models;
using BudgetBuddy.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BudgetBuddy.Sqlite.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public DatabaseContext DatabaseContext => (DatabaseContext)Context;

    public CategoryRepository(DbContext context)
        : base(context)
    {
    }
}