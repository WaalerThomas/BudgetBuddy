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

        RuleFor(x => x.MonthlyAmount)
            .Null()
            .When(x => x.IsGroup);
        RuleFor(x => x.MonthlyAmount)
            .GreaterThanOrEqualTo(0)
            .When(x => x.IsGroup == false);
        
        RuleFor(x => x.GoalAmount)
            .Null()
            .When(x => x.IsGroup);
        RuleFor(x => x.GoalAmount)
            .GreaterThanOrEqualTo(0)
            .When(x => x.IsGroup == false);
        
        RuleFor(x => x.GroupId)
            .NotNull()
            .When(x => x.IsGroup == false);
        RuleFor(x => x.GroupId)
            .Null()
            .When(x => x.IsGroup);
    }
}