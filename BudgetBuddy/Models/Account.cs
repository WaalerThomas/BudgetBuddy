namespace BudgetBuddy.Models;

public class Account
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public decimal SettledBalance { get; set; }
    public decimal PendingBalance { get; set; }

    public decimal ActualBalance => PendingBalance + SettledBalance;
}