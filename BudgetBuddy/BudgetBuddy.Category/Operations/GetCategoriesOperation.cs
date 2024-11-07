using BudgetBuddy.Category.Repositories;
using BudgetBuddy.Contracts.Model.Category;
using BudgetBuddy.Core.Operation;

namespace BudgetBuddy.Category.Operations;

public class GetCategoriesOperation : Operation<object, IEnumerable<CategoryModel>>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoriesOperation(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    protected override IEnumerable<CategoryModel> OnOperate(object request)
    {
        var categories = _categoryRepository.GetCategories();
        return categories;
    }
}