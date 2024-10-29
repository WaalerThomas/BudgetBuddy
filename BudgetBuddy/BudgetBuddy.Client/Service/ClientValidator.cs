using BudgetBuddy.Contracts.Interface.Common;
using BudgetBuddy.Contracts.Model.Client;
using BudgetBuddy.Core.Exceptions;
using FluentValidation;

namespace BudgetBuddy.Client.Service;

public class ClientValidator : AbstractValidator<ClientModel>, IClientValidator
{
    private readonly ICommonValidators _commonValidators;
    private readonly IClientService _clientService;
    private readonly IPasswordService _passwordService;
    
    public ClientValidator(ICommonValidators commonValidators,
        IClientService clientService,
        IPasswordService passwordService)
    {
        _commonValidators = commonValidators;
        _clientService = clientService;
        _passwordService = passwordService;
    }

    public bool ClientExists(Guid clientId, bool throwException = false)
    {
        var result = _clientService.Get(clientId);
        if (throwException && result == null)
        {
            throw new BuddyException("Client not found");
        }

        return result != null;
    }

    public void ValidateLogin(ClientModel client)
    {
        var clientModel = _clientService.GetByUsername(client.Username);
        if (clientModel == null)
        {
            throw new BuddyException("Invalid username or password");
        }

        if (clientModel.Salt == null)
        {
            throw new BuddyException("No salting exists for the client");
        }

        var isValidPassword = _passwordService.VerifyPassword(client.Password, clientModel.Password, clientModel.Salt);
        if (!isValidPassword)
        {
            throw new BuddyException("Invalid username or password");
        }
    }
}