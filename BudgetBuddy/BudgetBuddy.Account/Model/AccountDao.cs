using BudgetBuddy.Contracts.Enums;
using BudgetBuddy.Core.Database;

namespace BudgetBuddy.Account.Model;

public class AccountDao : BuddyDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int ClientId { get; set; }
    public AccountType Type { get; set; }
}