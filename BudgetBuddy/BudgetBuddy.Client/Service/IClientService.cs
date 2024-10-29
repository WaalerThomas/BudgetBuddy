using BudgetBuddy.Contracts.Model.Client;

namespace BudgetBuddy.Client.Service;

public interface IClientService
{
    ClientModel? Get(Guid id);
    ClientModel? GetByUsername(string username);
    ClientModel Login(ClientModel client);
}