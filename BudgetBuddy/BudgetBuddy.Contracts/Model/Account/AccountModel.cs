using BudgetBuddy.Contracts.Enums;
using BudgetBuddy.Contracts.Model.Common;

namespace BudgetBuddy.Contracts.Model.Account;

public class AccountModel : TimeKeepModel
{
    public int Id { get; set; }
    public Guid ClientId { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public AccountType Type { get; set; }
}