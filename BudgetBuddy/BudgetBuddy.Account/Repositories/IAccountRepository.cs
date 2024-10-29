using BudgetBuddy.Contracts.Model.Account;
using BudgetBuddy.Core.Repositories;

namespace BudgetBuddy.Account.Repositories;

public interface IAccountRepository : IBuddyRepository<AccountModel>
{
}