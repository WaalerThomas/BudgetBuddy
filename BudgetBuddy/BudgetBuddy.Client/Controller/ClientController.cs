using AutoMapper;
using BudgetBuddy.Client.Request;
using BudgetBuddy.Client.ViewModel;
using BudgetBuddy.Contracts.Interface.Client;
using BudgetBuddy.Contracts.Model.Client;
using BudgetBuddy.Contracts.Model.Common;
using BudgetBuddy.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BudgetBuddy.Client.Controller;

// Following this for validation: https://medium.com/@madu.sharadika/authentication-and-authorization-in-net-web-api-with-jwt-b46ef2f54e31
// TODO: Add inn login attempts and lock account after x attempts

[ApiController]
[Route("api/client")]
public class ClientController
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;
    private readonly IClientService _clientService;

    private const string AccessTokenCookieName = "prrrKeKeKedip";
    private const string RefreshTokenCookieName = "prrrKeKeKedipRefresh";
    
    public ClientController(
        IClientService clientService,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
    {
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
        
        httpContext.Response.Cookies.Append(AccessTokenCookieName, authenticationTokens.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = (DateTimeOffset)authenticationTokens.ExpiresIn
            });

        httpContext.Response.Cookies.Append(RefreshTokenCookieName, authenticationTokens.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = (DateTimeOffset)authenticationTokens.RefreshExpiresIn
            });
        
        return new BuddyResponse<AuthenticationInfoVm>(_mapper.Map<AuthenticationInfoVm>(authenticationTokens));
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
        
        httpContext.Response.Cookies.Delete(AccessTokenCookieName);
        httpContext.Response.Cookies.Delete(RefreshTokenCookieName);
        
        return new BuddyResponse<bool>(true);
    }
}
