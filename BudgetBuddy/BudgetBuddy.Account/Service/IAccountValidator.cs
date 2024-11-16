using BudgetBuddy.Contracts.Model.Account;
using FluentValidation;

namespace BudgetBuddy.Account.Service;

public interface IAccountValidator : IValidator<AccountModel>
{
    void ValidateNameUniqueness(AccountModel accountModel);
}