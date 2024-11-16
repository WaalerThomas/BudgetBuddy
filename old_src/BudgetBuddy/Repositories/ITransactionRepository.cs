using BudgetBuddy.Models;

namespace BudgetBuddy.Repositories;

public interface ITransactionRepository : IRepository<Transaction>
{
    IEnumerable<Transaction> GetAllWithExtra();
    IEnumerable<Transaction> GetAllPendingWithExtra();
    decimal GetCashFlowSum();
}