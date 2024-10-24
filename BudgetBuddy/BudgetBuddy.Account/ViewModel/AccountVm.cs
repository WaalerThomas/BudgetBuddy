using BudgetBuddy.Contracts.Enums;

namespace BudgetBuddy.Account.ViewModel;

public class AccountVm
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public AccountType Type { get; set; }
}