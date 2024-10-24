using BudgetBuddy.Contracts.Model.Account;
using FluentValidation;

namespace BudgetBuddy.Account.Service;

public class AccountValidator : AbstractValidator<AccountModel>, IAccountValidator
{
    public AccountValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}