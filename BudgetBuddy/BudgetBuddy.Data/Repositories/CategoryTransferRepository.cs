using BudgetBuddy.Common.Service;
using CategoryTransfer.Model;
using CategoryTransfer.Repositories;

namespace BudgetBuddy.Data.Repositories;

public class CategoryTransferRepository : Repository<CategoryTransferDao>, ICategoryTransferRepository
{
    public CategoryTransferRepository(DatabaseContext context, ICurrentUserService currentUser) : base(context, currentUser)
    {
    }

    public CategoryTransferDao Create(CategoryTransferDao model)
    {
        throw new NotImplementedException();
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
}