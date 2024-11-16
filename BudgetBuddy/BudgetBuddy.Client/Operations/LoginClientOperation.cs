using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BudgetBuddy.Client.Service;
using BudgetBuddy.Common.Service;
using BudgetBuddy.Contracts.Interface.Client;
using BudgetBuddy.Contracts.Model.Client;
using BudgetBuddy.Core.Exceptions;
using BudgetBuddy.Core.Operation;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BudgetBuddy.Client.Operations;

public class LoginClientOperation : Operation<ClientModel, string>
{
    private readonly IBuddyConfiguration _configuration;
    private readonly IClientValidator _clientValidator;
    private readonly IClientService _clientService;
    private readonly IPasswordService _passwordService;

    public LoginClientOperation(
        IClientValidator clientValidator,
        IBuddyConfiguration configuration,
        IClientService clientService,
        IPasswordService passwordService)
    {
        _clientValidator = clientValidator;
        _configuration = configuration;
        _clientService = clientService;
        _passwordService = passwordService;
    }

    protected override string OnOperate(ClientModel clientModel)
    {
        _clientValidator.ValidateAndThrow(clientModel);
        
        ValidateLogin(clientModel);
        
        var storedClient = _clientService.GetByUsername(clientModel.Username);
        var token = CreateToken(storedClient!.Id.ToString(), storedClient.Username);
        return token;
    }

    private void ValidateLogin(ClientModel client)
    {
        var clientModel = _clientService.GetByUsername(client.Username);
        if (clientModel is null)
        {
            throw new BuddyException("Invalid username or password");
        }

        if (clientModel.IsLockedOut)
        {
            throw new BuddyException("Account is locked out");
        }

        if (clientModel.LockoutExpired)
        {
            clientModel = _clientService.Unlock(clientModel.Id);
        }
        
        if (clientModel.Salt is null)
        {
            throw new BuddyException("No salting exists for the client");
        }

        var isValidPassword = _passwordService.Verify(client.Password, clientModel.Password, clientModel.Salt);
        if (!isValidPassword)
        {
            _clientService.IncrementFailedLoginAttempts(clientModel.Id);
            throw new BuddyException("Invalid username or password");
        }
    }

    private string CreateToken(string clientId, string username)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.JwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var securityToken = new JwtSecurityToken(_configuration.JwtIssuer,
            audience: _configuration.JwtIssuer,
            claims:
            [
                new Claim("client_id", clientId),
                new Claim("username", username)
            ],
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);

        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return token;
    }
}