using BudgetBuddy.Category.Repositories;
using BudgetBuddy.Category.Service;
using BudgetBuddy.Contracts.Model.Category;
using BudgetBuddy.Core.Operation;
using FluentValidation;

namespace BudgetBuddy.Category.Operations;

public class CreateCategoryOperation : Operation<CategoryModel, CategoryModel>
{
    private readonly ICategoryValidator _categoryValidator;
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryOperation(ICategoryValidator categoryValidator, ICategoryRepository categoryRepository)
    {
        _categoryValidator = categoryValidator;
        _categoryRepository = categoryRepository;
    }

    protected override CategoryModel OnOperate(CategoryModel categoryModel)
    {
        _categoryValidator.ValidateAndThrow(categoryModel);
        
        categoryModel = _categoryRepository.Create(categoryModel);
        
        return categoryModel;
    }
}