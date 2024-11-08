using BudgetBuddy.Contracts.Enums;
using BudgetBuddy.Core.Database;

namespace BudgetBuddy.Category.Model;

public class CategoryDao : BuddyDao
{
    public Guid ClientId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal? MonthlyAmount { get; set; }
    public decimal? GoalAmount { get; set; }
    public int? GroupId { get; set; }
    public CategoryType Type { get; set; }
}