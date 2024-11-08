namespace BudgetBuddy.Category.Request;

public class UpdateCategoryRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal? MonthlyAmount { get; set; }
    public decimal? GoalAmount { get; set; }
    public int? GroupId { get; set; }
}