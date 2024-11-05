using AutoMapper;
using BudgetBuddy.Account.Request;
using BudgetBuddy.Account.Service;
using BudgetBuddy.Account.ViewModel;
using BudgetBuddy.Contracts.Model.Account;
using BudgetBuddy.Contracts.Model.Common;
using BudgetBuddy.Core.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

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
    [EndpointSummary("Get all accounts")]
    [EndpointDescription("Get all accounts that the user has access to")]
    public BuddyResponse<IEnumerable<AccountVm>> Get()
    {
        var accountModels = _accountService.Get();
        var accounts = _mapper.Map<IEnumerable<AccountModel>, IEnumerable<AccountVm>>(accountModels);
        return new BuddyResponse<IEnumerable<AccountVm>>(accounts);
    }
    
    [NonAction]
    [HttpGet("{id:int}")]
    public BuddyResponse<AccountVm> Get(int id)
    {
        // TODO: Add authorization check to ensure user has access to account
        // TODO: Do we require this endpoint?
        
        var accountModel = _accountService.Get(id);
        if (accountModel == null)
        {
            throw new BuddyException("Account not found");
        }
        
        var account = _mapper.Map<AccountModel, AccountVm>(accountModel);
        return new BuddyResponse<AccountVm>(account);
    }

    [HttpPost]
    [EndpointSummary("Create a new account")]
    [EndpointDescription("Creates a new account for the user")]
    public BuddyResponse<AccountVm> Create(CreateAccountRequest createAccountRequest)
    {
        var accountModel = _mapper.Map<CreateAccountRequest, AccountModel>(createAccountRequest);
        accountModel = _accountService.Create(accountModel);
        
        return new BuddyResponse<AccountVm>(_mapper.Map<AccountModel, AccountVm>(accountModel));
    }
}