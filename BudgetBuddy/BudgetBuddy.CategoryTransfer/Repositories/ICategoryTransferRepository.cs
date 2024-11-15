using BudgetBuddy.Core.Repositories;
using CategoryTransfer.Model;

namespace CategoryTransfer.Repositories;

public interface ICategoryTransferRepository : IBuddyRepository<CategoryTransferDao>
{
    decimal GetFlowSum();
}