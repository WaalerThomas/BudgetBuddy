using BudgetBuddy.Contracts.Model.CategoryTransfer;

namespace BudgetBuddy.Contracts.Interface.CategoryTransfer;

public interface ICategoryTransferService
{
    CategoryTransferModel Create(CategoryTransferModel categoryTransferModel);
}