using BudgetBuddy.Category.Repositories;
using BudgetBuddy.Contracts.Model.Category;
using BudgetBuddy.Core.Operation;

namespace BudgetBuddy.Category.Operations;

public class GetCategoryByIdOperation : Operation<int, CategoryModel?>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryByIdOperation(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    protected override CategoryModel? OnOperate(int id)
    {
        var categoryModel = _categoryRepository.GetById(id);
        return categoryModel;
    }
}