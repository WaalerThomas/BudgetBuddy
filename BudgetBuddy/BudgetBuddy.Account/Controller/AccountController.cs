using AutoMapper;
using BudgetBuddy.Account.Service;
using BudgetBuddy.Account.ViewModel;
using BudgetBuddy.Contracts.Model.Account;
using BudgetBuddy.Contracts.Model.Common;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BudgetBuddy.Account.Controller;

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

    [HttpPost]
    public BuddyResponse<AccountVm> Create(AccountVm account)
    {
        var accountModel = _mapper.Map<AccountVm, AccountModel>(account);
        accountModel = _accountService.Create(accountModel);
        
        return new BuddyResponse<AccountVm>(_mapper.Map<AccountModel, AccountVm>(accountModel));
    }
}