using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BudgetBuddy.Client.Service;
using BudgetBuddy.Contracts.Model.Client;
using BudgetBuddy.Core.Operation;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BudgetBuddy.Client.Operations;

public class LoginClientOperation : Operation<ClientModel, string>
{
    private readonly IConfiguration _configuration;
    private readonly IClientValidator _clientValidator;

    public LoginClientOperation(IClientValidator clientValidator, IConfiguration configuration)
    {
        _clientValidator = clientValidator;
        _configuration = configuration;
    }

    protected override string OnOperate(ClientModel clientModel)
    {
        _clientValidator.ValidateAndThrow(clientModel);
        _clientValidator.ValidateLogin(clientModel);
        
        // TODO: Find a better way to get the key from the configuration
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var securityToken = new JwtSecurityToken(_configuration["Jwt:Issuer"],
            _configuration["Jwt:Issuer"],
            null,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);
        securityToken.Payload["client_id"] = clientModel.Id; // TODO: The Id would not be available in the client model
        securityToken.Payload["username"] = clientModel.Username;

        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return token;
    }
}