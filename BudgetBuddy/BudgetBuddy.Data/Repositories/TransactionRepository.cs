using BudgetBuddy.Common.Service;
using BudgetBuddy.Transaction.Model;
using BudgetBuddy.Transaction.Repositories;

namespace BudgetBuddy.Data.Repositories;

public class TransactionRepository : Repository<TransactionDao>, ITransactionRepository
{
    public TransactionRepository(
        DatabaseContext context, 
        ICurrentUserService currentUser) : base(context, currentUser)
    {
    }

    public TransactionDao Create(TransactionDao model)
    {
        throw new NotImplementedException();
    }

    public Task<TransactionDao> CreateAsync(TransactionDao model)
    {
        throw new NotImplementedException();
    }

    public TransactionDao Update(TransactionDao model)
    {
        throw new NotImplementedException();
    }

    public Task<TransactionDao> UpdateAsync(TransactionDao model)
    {
        throw new NotImplementedException();
    }

    public void Delete(TransactionDao model)
    {
        throw new NotImplementedException();
    }

    public TransactionDao? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TransactionDao> GetByIds(IEnumerable<int> ids)
    {
        throw new NotImplementedException();
    }
}