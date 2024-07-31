namespace BudgetBuddy.Models;

public class Category
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public decimal MonthlyAmount { get; set; }
    public decimal GoalAmount { get; set; }
    public Group? Group { get; set; }
}