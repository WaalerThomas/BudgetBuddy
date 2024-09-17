namespace BudgetBuddy.Domain.Entities.Categories;

public class Category
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public decimal MonthlyAmount { get; set; }
    public decimal GoalAmount { get; set; }

    public int CategoryGroupId { get; set; }
}