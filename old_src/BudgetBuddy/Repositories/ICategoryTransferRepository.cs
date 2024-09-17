using BudgetBuddy.Models;

namespace BudgetBuddy.Repositories;

public interface ICategoryTransferRepository : IRepository<CategoryTransfer>
{
    decimal GetCashFlowSum();
    IEnumerable<CategoryTransfer> GetAllWithExtra();
}