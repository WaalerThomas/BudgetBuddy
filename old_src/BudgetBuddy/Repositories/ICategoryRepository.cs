using BudgetBuddy.Models;

namespace BudgetBuddy.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
    decimal GetAvailableAmount(int id);
    decimal GetActivityAmount(int id);
    decimal GetBudgetetAmount(int id);
}