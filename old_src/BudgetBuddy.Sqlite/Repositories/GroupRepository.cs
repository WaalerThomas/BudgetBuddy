using BudgetBuddy.Models;
using BudgetBuddy.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BudgetBuddy.Sqlite.Repositories;

public class GroupRepository : Repository<Group>, IGroupRepository
{
    public DatabaseContext DatabaseContext => (DatabaseContext)Context;

    public GroupRepository(DbContext context)
        : base(context)
    {
    }

    public Group? GetWithCategories(int id)
    {
        return DatabaseContext.Groups.Include(g => g.Categories).SingleOrDefault(g => g.Id == id);
    }

    public IEnumerable<Group> GetAllWithCategories()
    {
        return DatabaseContext.Groups.Include(g => g.Categories).ToList();
    }
}