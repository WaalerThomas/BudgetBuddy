namespace BudgetBuddy.Models;

public class Category
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public decimal MonthlyAmount { get; set; }
    public decimal GoalAmount { get; set; }

    // TODO: Implement rest of this model

    public int GroupId { get; set; }
    public virtual Group? Group { get; set; }
}