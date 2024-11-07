using BudgetBuddy.Contracts.Interface.Common;
using BudgetBuddy.Contracts.Model.Category;
using FluentValidation;

namespace BudgetBuddy.Category.Service;

public class CategoryValidator : AbstractValidator<CategoryModel>, ICategoryValidator
{
    private readonly ICommonValidators _commonValidators;

    public CategoryValidator(ICommonValidators commonValidators)
    {
        _commonValidators = commonValidators;
        
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
}