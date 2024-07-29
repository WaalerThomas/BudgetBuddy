using BudgetBuddy.Repositories;
using BudgetBuddy.Sqlite.Repositories;

namespace BudgetBuddy.Sqlite;

public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _context;

    public IAccountRepository Accounts { get; private set; }

    public UnitOfWork(DatabaseContext context)
    {
        _context = context;
        Accounts = new AccountRespository(_context);
    }

    public int Complete()
    {
        return _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}