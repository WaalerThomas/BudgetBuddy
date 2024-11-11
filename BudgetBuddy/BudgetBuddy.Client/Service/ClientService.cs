using BudgetBuddy.Client.Operations;
using BudgetBuddy.Contracts.Interface.Client;
using BudgetBuddy.Contracts.Model.Client;
using BudgetBuddy.Core.Operation;
using BudgetBuddy.Core.Service;

namespace BudgetBuddy.Client.Service;

public class ClientService : ServiceBase, IClientService
{
    public ClientService(IOperationFactory operationFactory) : base(operationFactory)
    {
    }

    public ClientModel? Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public ClientModel? GetByUsername(string username)
    {
        var operation = CreateOperation<GetClientByUsernameOperation>();
        return operation.Operate(username);
    }

    public string Login(ClientModel client)
    {
        var operation = CreateOperation<LoginClientOperation>();
        return operation.Operate(client);
    }

    public ClientModel Create(ClientModel client)
    {
        var operation = CreateOperation<CreateClientOperation>();
        return operation.Operate(client);
    }
}