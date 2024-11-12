namespace BudgetBuddy.Core.Database;

public abstract class BuddyDao
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}