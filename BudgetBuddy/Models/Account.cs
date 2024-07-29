namespace BudgetBuddy.Models;

public class Account
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public decimal Balance { get; set; }
}