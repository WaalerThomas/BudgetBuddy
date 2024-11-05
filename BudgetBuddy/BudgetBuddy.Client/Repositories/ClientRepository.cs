using BudgetBuddy.Common.Database;
using BudgetBuddy.Common.Repositories;
using BudgetBuddy.Common.Service;
using BudgetBuddy.Contracts.Model.Client;

namespace BudgetBuddy.Client.Repositories;

public class ClientRepository : Repository<ClientModel>, IClientRepository
{
    public ClientRepository(
        DatabaseContext context,
        ICurrentUserService currentUserService) : base(context, currentUserService)
    {
    }

    public ClientModel Create(ClientModel model)
    {
        // TODO: What happens when saving fails??
        
        model.CreatedAt = DateTime.UtcNow;
        
        var result = Context.Clients.Add(model);
        Context.SaveChanges();
        return result.Entity;
    }

    public Task<ClientModel> CreateAsync(ClientModel model)
    {
        throw new NotImplementedException();
    }

    public ClientModel Update(ClientModel model)
    {
        throw new NotImplementedException();
    }

    public Task<ClientModel> UpdateAsync(ClientModel model)
    {
        throw new NotImplementedException();
    }

    public void Delete(ClientModel model)
    {
        throw new NotImplementedException();
    }

    public ClientModel GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ClientModel> GetByIds(IEnumerable<int> ids)
    {
        throw new NotImplementedException();
    }

    public ClientModel? GetByUsername(string username)
    {
        var clientModel = Context.Clients.FirstOrDefault(c => c.Username == username);
        return clientModel;
    }
}