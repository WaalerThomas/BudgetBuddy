using BudgetBuddy.Account.Repositories;
using BudgetBuddy.Account.Service;
using BudgetBuddy.Contracts.Model.Account;
using BudgetBuddy.Core.Operation;
using FluentValidation;

namespace BudgetBuddy.Account.Operations;

public class UpdateAccountOperation : Operation<AccountModel, AccountModel>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IAccountValidator _accountValidator;

    public UpdateAccountOperation(IAccountRepository accountRepository, IAccountValidator accountValidator)
    {
        _accountRepository = accountRepository;
        _accountValidator = accountValidator;
    }

    protected override AccountModel OnOperate(AccountModel accountModel)
    {
        _accountValidator.ValidateAndThrow(accountModel);
        
        // Does the client have access to this account?
        
        accountModel = _accountRepository.Update(accountModel);
        return accountModel;
    }
}