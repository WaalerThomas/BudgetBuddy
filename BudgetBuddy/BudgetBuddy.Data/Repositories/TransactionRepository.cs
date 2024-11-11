using BudgetBuddy.Common.Service;
using BudgetBuddy.Transaction.Model;
using BudgetBuddy.Transaction.Repositories;
using Microsoft.EntityFrameworkCore;

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
        model.ClientId = _currentUser.ClientId;
        model.CreatedAt = DateTime.Now;
        
        var result = Context.Transactions.Add(model);
        
        Context.SaveChanges();
        
        return result.Entity;
    }

    public Task<TransactionDao> CreateAsync(TransactionDao model)
    {
        throw new NotImplementedException();
    }

    public TransactionDao Update(TransactionDao model)
    {
        model.UpdatedAt = DateTime.Now;
        
        var result = Context.Transactions.Update(model);

        Context.SaveChanges();
        
        return result.Entity;
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
        return Context.Transactions
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == id && x.ClientId == _currentUser.ClientId);
    }

    public IEnumerable<TransactionDao> GetByIds(IEnumerable<int> ids)
    {
        throw new NotImplementedException();
    }
    
    public override IEnumerable<TransactionDao> GetAll()
    {
        return Context.Transactions
            .AsNoTracking()
            .Where(x => x.ClientId == _currentUser.ClientId)
            .ToList();
    }
}