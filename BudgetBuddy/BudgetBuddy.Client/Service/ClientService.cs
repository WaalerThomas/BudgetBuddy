﻿using BudgetBuddy.Client.Operations;
using BudgetBuddy.Contracts.Interface.Client;
using BudgetBuddy.Contracts.Model.Client;
using BudgetBuddy.Contracts.Response.Client;
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

    public AuthenticationTokensResponse Login(ClientModel client)
    {
        var operation = CreateOperation<LoginClientOperation>();
        return operation.Operate(client);
    }

    public ClientModel Create(ClientModel client)
    {
        var operation = CreateOperation<CreateClientOperation>();
        return operation.Operate(client);
    }

    public ClientModel Unlock(Guid id)
    {
        var operation = CreateOperation<UnlockClientOperation>();
        return operation.Operate(id);
    }

    public ClientModel IncrementFailedLoginAttempts(Guid id)
    {
        var operation = CreateOperation<IncrementFailedLoginAttemptsOperation>();
        return operation.Operate(id);
    }
}