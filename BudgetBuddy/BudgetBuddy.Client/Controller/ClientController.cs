using AutoMapper;
using BudgetBuddy.Client.Request;
using BudgetBuddy.Client.Service;
using BudgetBuddy.Client.ViewModel;
using BudgetBuddy.Contracts.Model.Client;
using BudgetBuddy.Contracts.Model.Common;
using BudgetBuddy.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BudgetBuddy.Client.Controller;

[ApiController]
[Route("api/client")]
public class ClientController
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly IClientService _clientService;
    
    public ClientController(IConfiguration configuration, IClientService clientService, IMapper mapper)
    {
        _configuration = configuration;
        _clientService = clientService;
        _mapper = mapper;
    }

    [HttpPost]
    public BuddyResponse<ClientVm> Create(CreateClientRequest createClientRequest)
    {
        // TODO: Implement password validation rules
        
        if (createClientRequest.Password != createClientRequest.ConfirmPassword)
        {
            throw new BuddyException("Passwords do not match");
        }
        
        var client = _mapper.Map<CreateClientRequest, ClientModel>(createClientRequest);
        client = _clientService.Create(client);
        
        return new BuddyResponse<ClientVm>(_mapper.Map<ClientModel, ClientVm>(client));
    }
    
    [HttpPost("login")]
    public BuddyResponse<string> Login(LoginClientRequest loginClientRequest)
    {
        var loginClient = _mapper.Map<LoginClientRequest, ClientModel>(loginClientRequest);
        var token = _clientService.Login(loginClient);
        return new BuddyResponse<string>(token);
    }
    
    [HttpPost("logout")]
    public BuddyResponse<bool> Logout()
    {
        throw new NotImplementedException();
    }
}