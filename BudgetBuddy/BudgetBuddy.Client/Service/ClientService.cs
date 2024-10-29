using BudgetBuddy.Contracts.Model.Client;
using FluentValidation;

namespace BudgetBuddy.Client.Service;

public class ClientService : IClientService
{

    private readonly IClientValidator _clientValidator;

    public ClientService(IClientValidator clientValidator)
    {
        _clientValidator = clientValidator;
    }

    public ClientModel? Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public ClientModel? GetByUsername(string username)
    {
        throw new NotImplementedException();
    }

    public ClientModel Login(ClientModel client)
    {
        _clientValidator.ValidateAndThrow(client);
        _clientValidator.ValidateLogin(client);
        
        // NOTE: Do we need to return the client model?
        return client;
    }
}