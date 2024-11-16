using BudgetBuddy.Category.Model;
using BudgetBuddy.Contracts.Enums;
using BudgetBuddy.Core.Repositories;

namespace BudgetBuddy.Category.Repositories;

public interface ICategoryRepository : IBuddyRepository<CategoryDao>
{
    IEnumerable<CategoryDao> GetByType(CategoryType type);
    IEnumerable<CategoryDao> GetGroupsCategories(int groupId);
}