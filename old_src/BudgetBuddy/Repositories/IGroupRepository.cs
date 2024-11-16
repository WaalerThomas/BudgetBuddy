using BudgetBuddy.Models;

namespace BudgetBuddy.Repositories;

public interface IGroupRepository : IRepository<Group>
{
    Group? GetWithCategories(int id);
    IEnumerable<Group> GetAllWithCategories();
}