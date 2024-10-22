using BudgetBuddy.Account.Model;

namespace BudgetBuddy.Account.Repository;

public class AccountRepository : IAccountRepository
{
    public AccountDao Create(AccountDao model)
    {
        throw new NotImplementedException();
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

    public IEnumerable<AccountDao> GetAll()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<AccountDao> GetByIds(IEnumerable<int> ids)
    {
        throw new NotImplementedException();
    }
}