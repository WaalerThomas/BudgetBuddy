using BudgetBuddy.Contracts.Enums;

namespace BudgetBuddy.Account.Request;

public class CreateAccountRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public AccountType Type { get; set; }
}