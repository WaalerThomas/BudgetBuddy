using BudgetBuddy.Category.Repositories;
using BudgetBuddy.Category.Service;
using BudgetBuddy.Contracts.Model.Category;
using BudgetBuddy.Core.Operation;
using FluentValidation;

namespace BudgetBuddy.Category.Operations;

public class UpdateCategoryOperation : Operation<CategoryModel, CategoryModel>
{
    private readonly ICategoryValidator _categoryValidator;
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryOperation(ICategoryRepository categoryRepository, ICategoryValidator categoryValidator)
    {
        _categoryRepository = categoryRepository;
        _categoryValidator = categoryValidator;
    }

    protected override CategoryModel OnOperate(CategoryModel categoryModel)
    {
        _categoryValidator.ValidateAndThrow(categoryModel);

        categoryModel = _categoryRepository.Update(categoryModel);
        return categoryModel;
    }
}