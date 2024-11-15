using BudgetBuddy.Contracts.Model.CategoryTransfer;
using FluentValidation;

namespace CategoryTransfer.Service;

public class CategoryTransferValidator : AbstractValidator<CategoryTransferModel>, ICategoryTransferValidator
{
    public CategoryTransferValidator()
    {
        RuleFor(x => x.FromCategoryId)
            .Null().WithMessage("From category id not needed when transferring from available to budget")
            .When(x => x.FromAvailableToBudget);
        
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than 0");    }
}