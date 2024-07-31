using BudgetBuddy.Models;
using BudgetBuddy.Repositories;

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
}