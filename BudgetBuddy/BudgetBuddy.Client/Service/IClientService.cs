using BudgetBuddy.Contracts.Model.Client;

namespace BudgetBuddy.Client.Service;

public interface IClientService
{
    ClientModel? Get(Guid id);
    ClientModel? GetByUsername(string username);
    string Login(ClientModel client);
    ClientModel Create(ClientModel client);
}