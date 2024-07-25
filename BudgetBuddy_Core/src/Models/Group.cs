namespace BudgetBuddyCore.Models;

public class Group
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<Category>? Categories { get; set; }

    public override string ToString() => Name;
}