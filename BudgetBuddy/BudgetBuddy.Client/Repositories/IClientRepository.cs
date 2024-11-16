using BudgetBuddy.Client.Model;
using BudgetBuddy.Core.Repositories;

namespace BudgetBuddy.Client.Repositories;

public interface IClientRepository : IBuddyRepository<ClientDao>
{
    ClientDao? GetByUsername(string username);
    ClientDao? GetById(Guid id);
}