using BudgetBuddy.Account.Operations;
using BudgetBuddy.Account.Repositories;
using BudgetBuddy.Contracts.Model.Account;
using BudgetBuddy.Core.Operation;
using BudgetBuddy.Core.Service;
using FluentValidation;

namespace BudgetBuddy.Account.Service;

public class AccountService : ServiceBase, IAccountService
{
    private readonly IAccountValidator _accountValidator;
    private readonly IAccountRepository _accountRepository;
    
    public AccountService(
        IOperationFactory operationFactory,
        IAccountRepository accountRepository,
        IAccountValidator accountValidator) : base(operationFactory)
    {
        _accountRepository = accountRepository;
        _accountValidator = accountValidator;
    }

    public AccountModel Create(AccountModel account)
    {
        _accountValidator.ValidateAndThrow(account);
        
        account = _accountRepository.Create(account);
        return account;
    }

    public AccountModel? Get(int id)
    {
        var accountModel = _accountRepository.GetById(id);
        return accountModel;
    }

    public IEnumerable<AccountModel> Get()
    {
        // TODO: There should probably be some kind of paging here
        
        var accountModels = _accountRepository.GetAll();
        return accountModels;
    }

    public AccountModel Update(AccountModel account)
    {
        var operation = CreateOperation<UpdateAccountOperation>();
        return operation.Operate(account);
    }
}