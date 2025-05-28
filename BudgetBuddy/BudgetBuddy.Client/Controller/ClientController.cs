using AutoMapper;
using BudgetBuddy.Client.Request;
using BudgetBuddy.Client.ViewModel;
using BudgetBuddy.Contracts.Interface.Client;
using BudgetBuddy.Contracts.Model.Client;
using BudgetBuddy.Contracts.Model.Common;
using BudgetBuddy.Core.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public BuddyResponse<AuthenticationInfoVm> Login(LoginClientRequest loginClientRequest)
    {
        var loginClient = _mapper.Map<LoginClientRequest, ClientModel>(loginClientRequest);
        var authenticationTokens = _clientService.Login(loginClient);
        
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext is null)
        {
            throw new BuddyException("HttpContext is null");
        }
        
        httpContext.Response.Cookies.Append("prrrKeKeKedip", authenticationTokens.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = (DateTimeOffset)authenticationTokens.ExpiresIn
            });

        httpContext.Response.Cookies.Append("prrrKeKeKedipRefresh", authenticationTokens.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = (DateTimeOffset)authenticationTokens.RefreshExpiresIn
            });
        
        var authenticationInfoVm = new AuthenticationInfoVm
        {
            AccessToken = token,
            RefreshToken = "",
            ExpiresIn = DateTime.Now.AddMinutes(30),
            Client = _mapper.Map<ClientVm>(loginClient)
        };
        
        return new BuddyResponse<AuthenticationInfoVm>(authenticationInfoVm);
    }
    
    [HttpPost("logout")]
    [EndpointSummary("Logout")]
    [EndpointDescription("Logout from the application")]
    public BuddyResponse<bool> Logout()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext is null)
        {
            throw new BuddyException("HttpContext is null");
        }
        
        httpContext.Response.Cookies.Delete("prrrKeKeKedip");
        httpContext.Response.Cookies.Delete("prrrKeKeKedipRefresh");
        
        return new BuddyResponse<bool>(true);
    }
    
    [Authorize]
    [HttpGet("me")]
    [EndpointSummary("Get current client")]
    [EndpointDescription("Get the current client")]
    public BuddyResponse<ClientVm> GetMe()
    {
        // var client = _clientService.GetMe();
        // return new BuddyResponse<ClientVm>(_mapper.Map<ClientModel, ClientVm>(client));
        return new BuddyResponse<ClientVm>();
    }
}
