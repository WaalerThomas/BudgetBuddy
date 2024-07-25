namespace BudgetBuddyCore.Models;

public class Category
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public decimal MonthlyAmount { get; set; }
    public decimal GoalAmount { get; set; }
    public required Group Group { get; set; }

    public override string ToString() => Name;
}