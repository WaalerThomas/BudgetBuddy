using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using BudgetBuddy.Client.Request;
using BudgetBuddy.Client.Service;
using BudgetBuddy.Client.ViewModel;
using BudgetBuddy.Contracts.Interface.Client;
using BudgetBuddy.Contracts.Model.Client;
using BudgetBuddy.Contracts.Model.Common;
using BudgetBuddy.Core.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BudgetBuddy.Client.Controller;

// Following this for validation: https://medium.com/@madu.sharadika/authentication-and-authorization-in-net-web-api-with-jwt-b46ef2f54e31
// TODO: Add a refresh token
// TODO: Add inn login attempts and lock account after x attempts

[ApiController]
[Route("api/client")]
public class ClientController
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly IClientService _clientService;
    
    public ClientController(
        IConfiguration configuration,
        IClientService clientService,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
    {
        _configuration = configuration;
        _clientService = clientService;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpPost]
    [EndpointSummary("Create a new client")]
    [EndpointDescription("Creates a new client")]
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
    [EndpointSummary("Login")]
    [EndpointDescription("Login to the application")]
    public BuddyResponse<string> Login(LoginClientRequest loginClientRequest)
    {
        var loginClient = _mapper.Map<LoginClientRequest, ClientModel>(loginClientRequest);
        var token = _clientService.Login(loginClient);
        
        // TODO: Add token to the response header instead of the body as cookie
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext is null)
        {
            throw new BuddyException("HttpContext is null");
        }
        
        httpContext.Response.Cookies.Append("prrrKeKeKedip", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.Now.AddMinutes(30)
            });
        
        return new BuddyResponse<string>(token);
    }
    
    [HttpPost("logout")]
    [EndpointSummary("Logout")]
    [EndpointDescription("Logout from the application")]
    public BuddyResponse<bool> Logout()
    {
        throw new NotImplementedException();
    }
}