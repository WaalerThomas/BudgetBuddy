using BudgetBuddy.Contracts.Enums;
using BudgetBuddy.Core.Database;

namespace BudgetBuddy.Account.Model;

public class AccountDao : BuddyDao
{
    public Guid ClientId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public AccountType Type { get; set; }
}