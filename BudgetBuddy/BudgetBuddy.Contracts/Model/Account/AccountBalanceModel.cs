namespace BudgetBuddy.Contracts.Model.Account;

public class AccountBalanceModel
{
    public int Id { get; set; }
    public decimal ActualBalance { get; set; }
    public decimal PendingBalance { get; set; }
    public decimal SettledBalance { get; set; }
}