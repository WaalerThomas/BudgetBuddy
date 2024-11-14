using AutoMapper;
using BudgetBuddy.Account.Model;
using BudgetBuddy.Account.Repositories;
using BudgetBuddy.Account.Service;
using BudgetBuddy.Contracts.Model.Account;
using BudgetBuddy.Core.Operation;
using FluentValidation;

namespace BudgetBuddy.Account.Operations;

public class CreateAccountOperation : Operation<AccountModel, AccountModel>
{
    private readonly IMapper _mapper;
    private readonly IAccountValidator _accountValidator;
    private readonly IAccountRepository _accountRepository;

    public CreateAccountOperation(
        IAccountValidator accountValidator,
        IAccountRepository accountRepository,
        IMapper mapper)
    {
        _accountValidator = accountValidator;
        _accountRepository = accountRepository;
        _mapper = mapper;
    }

    protected override AccountModel OnOperate(AccountModel account)
    {
        _accountValidator.ValidateAndThrow(account);
        _accountValidator.ValidateNameUniqueness(account);
        
        var accountDao = _mapper.Map<AccountModel, AccountDao>(account);
        accountDao = _accountRepository.Create(accountDao);
        
        account = _mapper.Map<AccountDao, AccountModel>(accountDao);
        return account;
    }
}