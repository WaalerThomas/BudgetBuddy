using BudgetBuddy.Category.Operations;
using BudgetBuddy.Contracts.Model.Category;
using BudgetBuddy.Core.Operation;
using BudgetBuddy.Core.Service;

namespace BudgetBuddy.Category.Service;

public class CategoryService : ServiceBase, ICategoryService
{
    public CategoryService(IOperationFactory operationFactory) : base(operationFactory)
    {
    }
    
    public CategoryModel Create(CategoryModel categoryModel)
    {
        var operation = CreateOperation<CreateCategoryOperation>();
        return operation.Operate(categoryModel);
    }
}