using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using BudgetBuddy.Client.Request;
using BudgetBuddy.Client.Service;
using BudgetBuddy.Client.ViewModel;
using BudgetBuddy.Contracts.Model.Client;
using BudgetBuddy.Contracts.Model.Common;
using BudgetBuddy.Core.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BudgetBuddy.Client.Controller;

[ApiController]
[Route("api/client")]
public class ClientController
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    
    // NOTE: These will be moved to service layer
    private readonly IClientService _clientService;
    private readonly IClientValidator _clientValidator;
    
    public ClientController(IConfiguration configuration, IClientService clientService,
        IClientValidator clientValidator, IMapper mapper)
    {
        _configuration = configuration;
        _clientService = clientService;
        _clientValidator = clientValidator;
        _mapper = mapper;
    }

    [HttpPost]
    public BuddyResponse<ClientVm> Create(CreateClientRequest createClientRequest)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost("login")]
    public BuddyResponse<string> Login(LoginClientRequest loginClientRequest)
    {
        var loginClient = _mapper.Map<LoginClientRequest, ClientModel>(loginClientRequest);
        loginClient = _clientService.Login(loginClient);
        
        // TODO: Shall we add the username inside the token to know which client requests are coming from?
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var sectoken = new JwtSecurityToken(_configuration["Jwt:Issuer"],
            _configuration["Jwt:Issuer"],
            null,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);

        var token = new JwtSecurityTokenHandler().WriteToken(sectoken);

        return new BuddyResponse<string>(token);
    }
    
    [HttpPost("logout")]
    public BuddyResponse<bool> Logout()
    {
        throw new NotImplementedException();
    }
}