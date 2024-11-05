using AutoMapper;
using BudgetBuddy.Account.Service;
using BudgetBuddy.Account.ViewModel;
using BudgetBuddy.Contracts.Model.Account;
using BudgetBuddy.Contracts.Model.Common;
using BudgetBuddy.Core.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetBuddy.Account.Controller;

[Authorize]
[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IAccountService _accountService;

    public AccountController(
        IAccountService accountService,
        IMapper mapper)
    {
        _accountService = accountService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public BuddyResponse<IEnumerable<AccountVm>> Get()
    {
        var accountModels = _accountService.Get();
        var accounts = _mapper.Map<IEnumerable<AccountModel>, IEnumerable<AccountVm>>(accountModels);
        return new BuddyResponse<IEnumerable<AccountVm>>(accounts);
    }
    
    [HttpGet("{id:int}")]
    public BuddyResponse<AccountVm> Get(int id)
    {
        var accountModel = _accountService.Get(id);
        if (accountModel == null)
        {
            throw new BuddyException("Account not found");
        }
        
        var account = _mapper.Map<AccountModel, AccountVm>(accountModel);
        return new BuddyResponse<AccountVm>(account);
    }

    [HttpPost]
    public BuddyResponse<AccountVm> Create(AccountVm account)
    {
        var accountModel = _mapper.Map<AccountVm, AccountModel>(account);
        accountModel = _accountService.Create(accountModel);
        
        account = _mapper.Map<AccountModel, AccountVm>(accountModel);
        return new BuddyResponse<AccountVm>(account);
    }
}