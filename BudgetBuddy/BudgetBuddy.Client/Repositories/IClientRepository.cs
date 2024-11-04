using BudgetBuddy.Contracts.Model.Client;
using BudgetBuddy.Core.Repositories;

namespace BudgetBuddy.Client.Repositories;

public interface IClientRepository : IBuddyRepository<ClientModel>
{
    ClientModel? GetByUsername(string username);
}