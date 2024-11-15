using BudgetBuddy.Contracts.Interface.CategoryTransfer;
using BudgetBuddy.Contracts.Model.CategoryTransfer;
using BudgetBuddy.Core.Operation;
using BudgetBuddy.Core.Service;
using CategoryTransfer.Operations;

namespace CategoryTransfer.Service;

public class CategoryTransferService : ServiceBase, ICategoryTransferService
{
    public CategoryTransferService(IOperationFactory operationFactory) : base(operationFactory)
    {
    }

    public CategoryTransferModel Create(CategoryTransferModel categoryTransferModel)
    {
        var operation = CreateOperation<CreateCategoryTransferOperation>();
        return operation.Operate(categoryTransferModel);
    }
}