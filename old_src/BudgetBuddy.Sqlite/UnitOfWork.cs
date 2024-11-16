using BudgetBuddy.Repositories;
using BudgetBuddy.Sqlite.Repositories;

namespace BudgetBuddy.Sqlite;

public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _context;

    public IAccountRepository Accounts { get; private set; }
    public IGroupRepository Groups { get; private set; }
    public ICategoryRepository Categories { get; private set; }
    public ITransactionRepository Transactions { get; private set; }
    public ICategoryTransferRepository CategoryTransfers { get; private set; }

    public UnitOfWork(DatabaseContext context)
    {
        _context = context;
        Accounts = new AccountRespository(_context);
        Groups = new GroupRepository(_context);
        Categories = new CategoryRepository(_context);
        Transactions = new TransactionRepository(_context);
        CategoryTransfers = new CategoryTransferRepository(_context);
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