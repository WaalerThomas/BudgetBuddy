using BudgetBuddy.Client.Model;
using BudgetBuddy.Client.Repositories;
using BudgetBuddy.Common.Service;
using Microsoft.EntityFrameworkCore;

namespace BudgetBuddy.Data.Repositories;

public class ClientRepository : Repository<ClientDao>, IClientRepository
{
    public ClientRepository(
        DatabaseContext context,
        ICurrentUserService currentUserService) : base(context, currentUserService)
    {
    }

    public ClientDao Create(ClientDao client)
    {
        // TODO: What happens when saving fails??
        
        client.CreatedAt = DateTime.UtcNow;
        
        var result = Context.Clients.Add(client);
        Context.SaveChanges();
        return result.Entity;
    }

    public Task<ClientDao> CreateAsync(ClientDao client)
    {
        throw new NotImplementedException();
    }

    public ClientDao Update(ClientDao client)
    {
        throw new NotImplementedException();
    }

    public Task<ClientDao> UpdateAsync(ClientDao client)
    {
        throw new NotImplementedException();
    }

    public void Delete(ClientDao client)
    {
        throw new NotImplementedException();
    }

    public ClientDao GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ClientDao> GetByIds(IEnumerable<int> ids)
    {
        throw new NotImplementedException();
    }

    public ClientDao? GetByUsername(string username)
    {
        var client = Context.Clients.AsNoTracking().FirstOrDefault(c => c.Username == username);
        return client;
    }
}