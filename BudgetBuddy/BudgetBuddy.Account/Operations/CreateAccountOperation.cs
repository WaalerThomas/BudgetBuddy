using BudgetBuddy.Account.Repositories;
using BudgetBuddy.Account.Service;
using BudgetBuddy.Contracts.Model.Account;
using BudgetBuddy.Core.Operation;
using FluentValidation;

namespace BudgetBuddy.Account.Operations;

public class CreateAccountOperation : Operation<AccountModel, AccountModel>
{
    private readonly IAccountValidator _accountValidator;
    private readonly IAccountRepository _accountRepository;

    public CreateAccountOperation(IAccountValidator accountValidator, IAccountRepository accountRepository)
    {
        _accountValidator = accountValidator;
        _accountRepository = accountRepository;
    }

    protected override AccountModel OnOperate(AccountModel account)
    {
        _accountValidator.ValidateAndThrow(account);
        
        account = _accountRepository.Create(account);
        return account;
    }
}