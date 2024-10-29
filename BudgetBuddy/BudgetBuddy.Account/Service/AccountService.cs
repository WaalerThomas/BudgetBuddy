using AutoMapper;
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
}