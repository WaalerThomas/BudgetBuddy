using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BudgetBuddy.Client.Service;
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
    private readonly IConfiguration _configuration;
    private readonly IClientValidator _clientValidator;
    private readonly IClientService _clientService;
    private readonly IPasswordService _passwordService;

    public LoginClientOperation(
        IClientValidator clientValidator,
        IConfiguration configuration,
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

        if (clientModel.Salt is null)
        {
            throw new BuddyException("No salting exists for the client");
        }

        if (client.IsLockedOut)
        {
            throw new BuddyException("Account is locked out");
        }

        if (client.LockoutExpired)
        {
            client = _clientService.Unlock(clientModel.Id);
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
        // TODO: Find a better way to get the key from the configuration
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // TODO: Add the client id and username to the token through claims
        
        var securityToken = new JwtSecurityToken(_configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Issuer"],
            claims: null,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);
        
        securityToken.Payload["client_id"] = clientId;
        securityToken.Payload["username"] = username;

        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return token;
    }
}