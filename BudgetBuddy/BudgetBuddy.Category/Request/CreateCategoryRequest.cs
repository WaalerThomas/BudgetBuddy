using BudgetBuddy.Contracts.Enums;

namespace BudgetBuddy.Category.Request;

public class CreateCategoryRequest
{
    public required string Name { get; set; }
    public decimal? MonthlyAmount { get; set; }
    public decimal? GoalAmount { get; set; }
    public CategoryType Type { get; set; }
    public int? GroupId { get; set; }
}