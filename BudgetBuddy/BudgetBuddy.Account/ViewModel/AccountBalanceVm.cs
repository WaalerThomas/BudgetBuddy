namespace BudgetBuddy.Account.ViewModel;

public class AccountBalanceVm
{
    public int AccountId { get; set; }
    public decimal ActualBalance { get; set; }
    public decimal PendingBalance { get; set; }
    public decimal SettledBalance { get; set; }
}