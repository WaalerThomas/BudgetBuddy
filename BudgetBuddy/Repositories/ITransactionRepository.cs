using System.Reflection.Metadata.Ecma335;
using BudgetBuddy.Models;

namespace BudgetBuddy.Repositories;

public interface ITransactionRepository : IRepository<Transaction>
{
    IEnumerable<Transaction> GetAllWithExtra();
    decimal GetCashFlowSum();
}