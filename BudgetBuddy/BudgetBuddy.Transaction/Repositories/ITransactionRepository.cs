using BudgetBuddy.Core.Repositories;
using BudgetBuddy.Transaction.Model;

namespace BudgetBuddy.Transaction.Repositories;

public interface ITransactionRepository : IBuddyRepository<TransactionDao>
{
}