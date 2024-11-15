using BudgetBuddy.Common.Service;
using CategoryTransfer.Model;
using CategoryTransfer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BudgetBuddy.Data.Repositories;

public class CategoryTransferRepository : Repository<CategoryTransferDao>, ICategoryTransferRepository
{
    public CategoryTransferRepository(DatabaseContext context, ICurrentUserService currentUser) : base(context, currentUser)
    {
    }

    public CategoryTransferDao Create(CategoryTransferDao model)
    {
        model.CreatedAt = DateTime.UtcNow;
        model.ClientId = _currentUser.ClientId;

        var result = Context.CategoryTransfers.Add(model);

        Context.SaveChanges();

        return result.Entity;
    }

    public Task<CategoryTransferDao> CreateAsync(CategoryTransferDao model)
    {
        throw new NotImplementedException();
    }

    public CategoryTransferDao Update(CategoryTransferDao model)
    {
        throw new NotImplementedException();
    }

    public Task<CategoryTransferDao> UpdateAsync(CategoryTransferDao model)
    {
        throw new NotImplementedException();
    }

    public void Delete(CategoryTransferDao model)
    {
        throw new NotImplementedException();
    }

    public CategoryTransferDao? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<CategoryTransferDao> GetByIds(IEnumerable<int> ids)
    {
        throw new NotImplementedException();
    }

    public decimal GetFlowSum()
    {
        return Context.CategoryTransfers
            .AsNoTracking()
            .Where(x => x.ClientId == _currentUser.ClientId)
            .Select(x => x.Amount)
            .ToList()
            .Sum();
    }
}