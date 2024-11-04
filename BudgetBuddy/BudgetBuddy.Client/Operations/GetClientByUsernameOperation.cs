using BudgetBuddy.Client.Repositories;
using BudgetBuddy.Contracts.Model.Client;
using BudgetBuddy.Core.Operation;

namespace BudgetBuddy.Client.Operations;

public class GetClientByUsernameOperation : Operation<string, ClientModel?>
{
    private readonly IClientRepository _clientRepository;

    public GetClientByUsernameOperation(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    protected override ClientModel? OnOperate(string username)
    {
        var clientModel = _clientRepository.GetByUsername(username);
        return clientModel;
    }
}