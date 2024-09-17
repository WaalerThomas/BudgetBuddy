using BudgetBuddy.Domain.Common;

namespace BudgetBuddy.Domain.Services;

public interface ICategoryGroupService
{
    Result<int> CreateCategoryGroup(string name);
}