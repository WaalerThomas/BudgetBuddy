using AutoMapper;
using BudgetBuddy.Account.Model;
using BudgetBuddy.Account.Repositories;
using BudgetBuddy.Contracts.Model.Account;
using FluentValidation;

namespace BudgetBuddy.Account.Service;

public class AccountService : IAccountService
{
    private readonly IMapper _mapper;
    private readonly IAccountValidator _accountValidator;
    private readonly IAccountRepository _accountRepository;
    
    public AccountService(
        IAccountRepository accountRepository,
        IMapper mapper,
        IAccountValidator accountValidator)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
        _accountValidator = accountValidator;
    }

    public AccountModel Create(AccountModel account)
    {
        _accountValidator.ValidateAndThrow(account);
        var accountDao = _mapper.Map<AccountModel, AccountDao>(account);
        
        accountDao = _accountRepository.Create(accountDao);
        account = _mapper.Map<AccountDao, AccountModel>(accountDao);
        
        return account;
    }
}