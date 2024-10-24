using BudgetBuddy.Account.Model;
using BudgetBuddy.Core.Repository;

namespace BudgetBuddy.Account.Repositories;

public interface IAccountRepository : IBuddyRepository<AccountDao>
{
}