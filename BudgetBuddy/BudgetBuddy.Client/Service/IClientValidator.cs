using BudgetBuddy.Contracts.Model.Client;
using FluentValidation;

namespace BudgetBuddy.Client.Service;

public interface IClientValidator : IValidator<ClientModel>
{
    bool ClientExists(Guid clientId, bool throwException = false);

    void ValidateLogin(ClientModel client);
}