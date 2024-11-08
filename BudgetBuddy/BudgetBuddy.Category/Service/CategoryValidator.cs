using BudgetBuddy.Category.Repositories;
using BudgetBuddy.Contracts.Interface.Common;
using BudgetBuddy.Contracts.Model.Category;
using BudgetBuddy.Core.Exceptions;
using FluentValidation;

namespace BudgetBuddy.Category.Service;

public class CategoryValidator : AbstractValidator<CategoryModel>, ICategoryValidator
{
    private readonly ICommonValidators _commonValidators;
    private readonly ICategoryRepository _categoryRepository;

    public CategoryValidator(ICommonValidators commonValidators, ICategoryRepository categoryRepository)
    {
        _commonValidators = commonValidators;
        _categoryRepository = categoryRepository;

        RuleFor(x => x.Name).NotEmpty();

        // ### Validation Rules for Groups ###
        RuleFor(x => x.MonthlyAmount)
            .Null()
            .When(x => x.IsGroup)
            .WithMessage("Groups cannot have a monthly amount");
        
        RuleFor(x => x.GoalAmount)
            .Null()
            .When(x => x.IsGroup)
            .WithMessage("Groups cannot have a goal amount");
        
        RuleFor(x => x.GroupId)
            .Null()
            .When(x => x.IsGroup)
            .WithMessage("Groups cannot belong to a group");
        
        // ### Validation Rules for Categories ###
        RuleFor(x => x.MonthlyAmount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Monthly amount needs to be a positive number")
            .NotNull()
            .When(x => x.IsGroup == false);

        RuleFor(x => x.GoalAmount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Goal amount needs to be a positive number")
            .NotNull()
            .When(x => x.IsGroup == false);
        
        RuleFor(x => x.GroupId)
            .NotNull()
            .When(x => x.IsGroup == false)
            .WithMessage("Categories must belong to a group");
    }

    public void ValidateGroupAssignment(CategoryModel categoryModel)
    {
        if (categoryModel.IsGroup)
        {
            return;
        }
        
        // This is also checked in the FluentValidation rules
        if (categoryModel.GroupId is null)
        {
            throw new BuddyException("Categories must belong to a group");
        }
        
        var categoryGroup = _categoryRepository.GetById(categoryModel.GroupId.Value);
        
        if (categoryGroup is null)
        {
            throw new BuddyException("Category group does not exist.");
        }

        if (categoryGroup.IsGroup == false)
        {
            throw new BuddyException("Cannot assign a category to another category");
        }
    }
}