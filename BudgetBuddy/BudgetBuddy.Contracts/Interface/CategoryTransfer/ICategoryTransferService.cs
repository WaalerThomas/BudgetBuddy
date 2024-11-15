using BudgetBuddy.Contracts.Model.CategoryTransfer;

namespace BudgetBuddy.Contracts.Interface.CategoryTransfer;

public interface ICategoryTransferService
{
    decimal GetAvailableToBudget();
    CategoryTransferModel Create(CategoryTransferModel categoryTransferModel);
}