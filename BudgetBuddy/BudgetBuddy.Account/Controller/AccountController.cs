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

    [HttpPost]
    [EndpointSummary("Create a new account")]
    [EndpointDescription("Creates a new account for the user")]
    public BuddyResponse<AccountVm> Create([FromBody] CreateAccountRequest createAccountRequest)
    {
        var accountModel = _mapper.Map<CreateAccountRequest, AccountModel>(createAccountRequest);
        accountModel = _accountService.Create(accountModel);
        
        return new BuddyResponse<AccountVm>(_mapper.Map<AccountModel, AccountVm>(accountModel));
    }
    
    [HttpPatch]
    [EndpointSummary("Update an account")]
    [EndpointDescription("Updates an existing account for the user")]
    public BuddyResponse<AccountVm> Update([FromBody] UpdateAccountRequest updateAccountRequest)
    {
        var accountModel = _accountService.Get(updateAccountRequest.Id);
        if (accountModel is null)
        {
            throw new BuddyException("Account not found");
        }
        
        _mapper.Map(updateAccountRequest, accountModel);
        
        accountModel = _accountService.Update(accountModel);
        return new BuddyResponse<AccountVm>(_mapper.Map<AccountModel, AccountVm>(accountModel));
    }
}