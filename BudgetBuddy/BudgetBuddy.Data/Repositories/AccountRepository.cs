using AutoMapper;
using BudgetBuddy.Account.Model;
using BudgetBuddy.Account.Repositories;
using BudgetBuddy.Common.Service;

namespace BudgetBuddy.Data.Repositories;

public class AccountRepository : Repository<AccountDao>, IAccountRepository
{
    private readonly IMapper _mapper;

    public AccountRepository(
        DatabaseContext context,
        IMapper mapper,
        ICurrentUserService currentUserService) : base(context, currentUserService)
    {
        _mapper = mapper;
    }
    
    public AccountDao Create(AccountDao account)
    {
        // TODO: What happens when saving fails??
        
        account.ClientId = _currentUser.ClientId;
        account.CreatedAt = DateTime.UtcNow;
        
        var result = Context.Accounts.Add(account);
        
        Context.SaveChanges();
        
        return result.Entity;
    }

    public Task<AccountDao> CreateAsync(AccountDao account)
    {
        throw new NotImplementedException();
    }

    public AccountDao Update(AccountDao account)
    {
        // TODO: Implement history table to add the old values to the history table
        // TODO: Log the changes somewhere?
        
        account.UpdatedAt = DateTime.UtcNow;
        
        var result = Context.Accounts.Update(account);

        Context.SaveChanges();

        return result.Entity;
    }

    public Task<AccountDao> UpdateAsync(AccountDao account)
    {
        throw new NotImplementedException();
    }

    public void Delete(AccountDao account)
    {
        throw new NotImplementedException();
    }

    public AccountDao? GetById(int id)
    {
        return Context.Accounts.FirstOrDefault(x => x.ClientId == _currentUser.ClientId && x.Id == id);
    }

    public IEnumerable<AccountDao> GetByIds(IEnumerable<int> ids)
    {
        throw new NotImplementedException();
    }

    public override IEnumerable<AccountDao> GetAll()
    {
        return Context.Accounts
            .Where(x => x.ClientId == _currentUser.ClientId)
            .ToList();
    }
}