using BudgetBuddy.Category.Repositories;
using BudgetBuddy.Contracts.Model.Category;
using BudgetBuddy.Core.Operation;

namespace BudgetBuddy.Category.Operations;

public class GetGroupsOperation : Operation<object, IEnumerable<CategoryModel>>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetGroupsOperation(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    protected override IEnumerable<CategoryModel> OnOperate(object request)
    {
        var groups = _categoryRepository.GetByType(true);
        return groups;
    }
}