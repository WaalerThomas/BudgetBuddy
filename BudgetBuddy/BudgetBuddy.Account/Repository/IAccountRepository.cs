using BudgetBuddy.Account.Model;
using BudgetBuddy.Core.Repository;

namespace BudgetBuddy.Account.Repository;

public interface IAccountRepository : IBuddyRepository<AccountDao>
{
}