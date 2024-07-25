namespace BudgetBuddyCore.Models;

public class Account
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public decimal Balance { get; set; }

    public override string ToString() => Name;
}