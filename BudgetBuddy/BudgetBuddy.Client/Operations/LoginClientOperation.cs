using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BudgetBuddy.Client.Repositories;
using BudgetBuddy.Client.Service;
using BudgetBuddy.Contracts.Model.Client;
using BudgetBuddy.Core.Operation;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BudgetBuddy.Client.Operations;

public class LoginClientOperation : Operation<ClientModel, string>
{
    private readonly IConfiguration _configuration;
    private readonly IClientValidator _clientValidator;
    private readonly IClientService _clientService;

    public LoginClientOperation(
        IClientValidator clientValidator,
        IConfiguration configuration,
        IClientService clientService)
    {
        _clientValidator = clientValidator;
        _configuration = configuration;
        _clientService = clientService;
    }

    protected override string OnOperate(ClientModel clientModel)
    {
        _clientValidator.ValidateAndThrow(clientModel);
        _clientValidator.ValidateLogin(clientModel);
        
        var storedClient = _clientService.GetByUsername(clientModel.Username);
        
        // TODO: Find a better way to get the key from the configuration
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // TODO: Add the client id and username to the token through claims
        
        var securityToken = new JwtSecurityToken(_configuration["Jwt:Issuer"],
            _configuration["Jwt:Issuer"],
            null,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);
        securityToken.Payload["client_id"] = storedClient!.Id; // TODO: The Id would not be available in the client model
        securityToken.Payload["username"] = storedClient.Username;

        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return token;
    }
}