using BudgetBuddy.Category.Repositories;
using BudgetBuddy.Category.Resources;
using BudgetBuddy.Contracts.Enums;
using BudgetBuddy.Contracts.Model.Category;
using BudgetBuddy.Core.Exceptions;
using FluentValidation;

namespace BudgetBuddy.Category.Service;

public class CategoryValidator : AbstractValidator<CategoryModel>, ICategoryValidator
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryValidator(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;

        RuleFor(x => x.Name).NotEmpty();

        // ### Validation Rules for Groups ###
        RuleFor(x => x.MonthlyAmount)
            .Null().WithMessage(CategoryResource.GroupNeedNullMonthlyAmount)
            .When(x => x.IsGroup);

        RuleFor(x => x.GoalAmount)
            .Null().WithMessage(CategoryResource.GroupNeedNullGoalAmount)
            .When(x => x.IsGroup);

        RuleFor(x => x.GroupId)
            .Null().WithMessage(CategoryResource.AssigningGroupToGroup)
            .When(x => x.IsGroup);
        
        // ### Validation Rules for Categories ###
        RuleFor(x => x.MonthlyAmount)
            .GreaterThanOrEqualTo(0).WithMessage(CategoryResource.NeedPositiveMonthlyAmount)
            .NotNull().WithMessage(CategoryResource.MonthlyAmountRequired)
            .When(x => x.IsCategory);

        RuleFor(x => x.GoalAmount)
            .GreaterThanOrEqualTo(0).WithMessage(CategoryResource.NeedPositiveGoalAmount)
            .When(x => x.IsCategory);

        RuleFor(x => x.GroupId)
            .NotNull().WithMessage(CategoryResource.GroupIdIsMissing)
            .When(x => x.IsCategory);
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
            throw new BuddyException(CategoryResource.GroupIdIsMissing);
        }
        
        var categoryGroup = _categoryRepository.GetById(categoryModel.GroupId.Value);
        
        if (categoryGroup is null)
        {
            throw new BuddyException(CategoryResource.CategoryGroupDoesNotExist);
        }

        if (categoryGroup.Type == CategoryType.Category)
        {
            throw new BuddyException(CategoryResource.AssigningCategoryToCategory);
        }
    }
}