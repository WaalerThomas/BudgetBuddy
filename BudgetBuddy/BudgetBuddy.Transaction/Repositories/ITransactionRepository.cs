using BudgetBuddy.Contracts.Model.Account;
using BudgetBuddy.Core.Repositories;
using BudgetBuddy.Transaction.Model;

namespace BudgetBuddy.Transaction.Repositories;

public interface ITransactionRepository : IBuddyRepository<TransactionDao>
{
    AccountBalanceModel GetBalance(int accountId, bool onlyActualBalance);
}