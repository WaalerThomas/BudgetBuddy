using BudgetBuddy.Contracts.Interface.Client;
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
}