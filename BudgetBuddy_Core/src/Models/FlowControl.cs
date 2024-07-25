namespace BudgetBuddyCore.Models;

public class FlowControl
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public override string ToString() => Name;
}