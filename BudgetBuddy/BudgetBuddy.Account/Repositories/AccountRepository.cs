using AutoMapper;
using BudgetBuddy.Account.Model;
using BudgetBuddy.Common.Database;
using BudgetBuddy.Common.Repositories;
using BudgetBuddy.Contracts.Model.Account;
using Microsoft.EntityFrameworkCore;

namespace BudgetBuddy.Account.Repositories;

public class AccountRepository : Repository<AccountDao>, IAccountRepository
{
    private readonly IMapper _mapper;
    public AccountRepository(DatabaseContext context, IMapper mapper) : base(context)
    {
        _mapper = mapper;
    }

    public AccountDao Create(AccountDao model)
    {
        // TODO: What happens when saving fails??
        // TODO: What is the point of using DAO's here? 
        
        model.CreatedAt = DateTime.UtcNow;
        
        var result = Context.Accounts.Add(_mapper.Map<AccountDao, AccountModel>(model));
        Context.SaveChanges();
        return _mapper.Map<AccountModel, AccountDao>(result.Entity);
    }

    public Task<AccountDao> CreateAsync(AccountDao model)
    {
        throw new NotImplementedException();
    }

    public AccountDao Update(AccountDao model)
    {
        throw new NotImplementedException();
    }

    public Task<AccountDao> UpdateAsync(AccountDao model)
    {
        throw new NotImplementedException();
    }

    public void Delete(AccountDao model)
    {
        throw new NotImplementedException();
    }

    public AccountDao GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<AccountDao> GetByIds(IEnumerable<int> ids)
    {
        throw new NotImplementedException();
    }
}