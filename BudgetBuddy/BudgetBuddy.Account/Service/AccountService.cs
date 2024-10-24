using AutoMapper;
using BudgetBuddy.Account.Model;
using BudgetBuddy.Account.Repositories;
using BudgetBuddy.Contracts.Model.Account;

namespace BudgetBuddy.Account.Service;

public class AccountService : IAccountService
{
    private readonly IMapper _mapper;
    private readonly IAccountRepository _accountRepository;
    
    public AccountService(IAccountRepository accountRepository, IMapper mapper)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
    }

    public AccountModel Create(AccountModel account)
    {
        // TODO: Implement validation
        
        var accountDao = _accountRepository.Create(_mapper.Map<AccountModel, AccountDao>(account));
        return _mapper.Map<AccountDao, AccountModel>(accountDao);
    }
}