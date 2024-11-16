using BudgetBuddy.Models;
using BudgetBuddy.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BudgetBuddy.Sqlite.Repositories;

public class TransactionRepository : Repository<Transaction>, ITransactionRepository
{
    public DatabaseContext DatabaseContext => (DatabaseContext)Context;

    public TransactionRepository(DatabaseContext context)
        : base(context)
    {
    }

    public override void Add(Transaction transaction)
    {
        // Need to attach the status and type enums so that EF doesn't try to add into tables
        DatabaseContext.TransactionStatuses.Attach(transaction.TransactionStatus);
        DatabaseContext.TransactionTypes.Attach(transaction.TransactionType);

        DatabaseContext.Transactions.Add(transaction);
    }

    public decimal GetCashFlowSum()
    {
        var cashflowEntries = DatabaseContext.Transactions
            .Where(t => t.TransactionType.Id == TransactionTypeEnum.BalanceAdjustment)
            .Select(t => t.Amount)
            .ToList().Sum();

        return cashflowEntries;
    }

    public IEnumerable<Transaction> GetAllWithExtra()
    {
        return DatabaseContext.Transactions
            .Include(t => t.TransactionType)
            .Include(t => t.Category)
            .Include(t => t.Account)
            .Include(t => t.TransactionStatus)
            .ToList();
    }

    public IEnumerable<Transaction> GetAllPendingWithExtra()
    {
        return DatabaseContext.Transactions
            .Where(t => t.TransactionStatus.Id == TransactionStatusEnum.Pending)
            .Include(t => t.TransactionType)
            .Include(t => t.Category)
            .Include(t => t.Account)
            .Include(t => t.TransactionStatus)
            .ToList();
    }
}