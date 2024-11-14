using BudgetBuddy.Account.Repositories;
using BudgetBuddy.Account.Resources;
using BudgetBuddy.Contracts.Model.Account;
using BudgetBuddy.Core.Exceptions;
using FluentValidation;

namespace BudgetBuddy.Account.Service;

public class AccountValidator : AbstractValidator<AccountModel>, IAccountValidator
{
    private readonly IAccountRepository _accountRepository;
    
    public AccountValidator(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
        
        RuleFor(x => x.Name).NotEmpty().WithMessage(AccountResource.NameRequired);
        RuleFor(x => x.Type).IsInEnum().WithMessage(AccountResource.AccountTypeRequired);
    }

    public void ValidateNameUniqueness(AccountModel accountModel)
    {
        var existingAccount = _accountRepository.GetByName(accountModel.Name);
        if (existingAccount is not null && existingAccount.Id != accountModel.Id)
        {
            throw new BuddyException(AccountResource.AccountNameNotUnique);
        }
    }
}