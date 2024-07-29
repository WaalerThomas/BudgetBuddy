namespace BudgetBuddy.Models;

public class Group
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public virtual List<Category> Categories { get; } = new();
}