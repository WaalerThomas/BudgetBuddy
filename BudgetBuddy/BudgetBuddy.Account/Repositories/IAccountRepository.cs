using BudgetBuddy.Account.Model;
using BudgetBuddy.Core.Repositories;

namespace BudgetBuddy.Account.Repositories;

public interface IAccountRepository : IBuddyRepository<AccountDao>
{
    AccountDao? GetByName(string name);
}