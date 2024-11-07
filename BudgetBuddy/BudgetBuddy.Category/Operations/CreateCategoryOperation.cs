using BudgetBuddy.Category.Repositories;
using BudgetBuddy.Category.Service;
using BudgetBuddy.Contracts.Model.Category;
using BudgetBuddy.Core.Exceptions;
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
        
        // If this is a category, check if the group exists
        if (categoryModel.GroupId is not null)
        {
            var categoryGroup = _categoryRepository.GetById(categoryModel.GroupId.Value);
            if (categoryGroup is null)
            {
                throw new BuddyException("Category group does not exist.");
            }
        }
        
        categoryModel = _categoryRepository.Create(categoryModel);
        
        return categoryModel;
    }
}