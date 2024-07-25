using BudgetBuddyCore.Models;

namespace BudgetBuddyCore.Interfaces;

public interface ITransactionRepository
{
    IEnumerable<Transaction> GetTransactions();
    Transaction? GetTransaction(int id);
    DateOnly? GetLastReconciliation();
    bool Insert(Transaction transaction);
    bool Delete(int id);
    bool Update(Transaction transaction);
}