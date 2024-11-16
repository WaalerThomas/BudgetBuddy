using BudgetBuddy.Contracts.Model.Client;

namespace BudgetBuddy.Contracts.Interface.Client;

public interface IClientService
{
    ClientModel? Get(Guid id);
    ClientModel? GetByUsername(string username);
    string Login(ClientModel client);
    ClientModel Create(ClientModel client);
    ClientModel Unlock(Guid id);
    ClientModel IncrementFailedLoginAttempts(Guid id);
}