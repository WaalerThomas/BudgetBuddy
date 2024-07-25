using BudgetBuddyCore.Interfaces;
using BudgetBuddyCore.Models;
using Dapper;

namespace BudgetBuddySqlite;

public class TransactionRepository : ITransactionRepository
{
    private readonly DatabaseContext _context;

    public TransactionRepository(DatabaseContext context)
    {
        _context = context;
    }

    public bool Delete(int id)
    {
        throw new NotImplementedException();
    }

    public DateOnly? GetLastReconciliation()
    {
        throw new NotImplementedException();
    }

    public Transaction? GetTransaction(int id)
    {
        return _context.GetConnection().QuerySingleOrDefault<Transaction>("SELECT * FROM budget_transaction WHERE id = @id", new { id });
    }

    public IEnumerable<Transaction> GetTransactions()
    {
        return _context.GetConnection().Query<Transaction>("SELECT * FROM budget_transaction");
    }

    public bool Insert(Transaction transaction)
    {
        int rowsInserted = _context.GetConnection().Execute(
            @"
                INSERT INTO budget_transaction (date, amount, category_id, account_id, memo, flow_control_id, status_id)
                VALUES (@Date, @Amount, @CategoryId, @AccountId, @Memo, @FlowControlId, @TransactionStatus)
            ",
            new {
                Date = transaction.Date.ToString("yyyy-MM-dd"),
                transaction.Amount,
                CategoryId = transaction.Category.Id,
                AccountId = transaction.Account.Id,
                transaction.Memo,
                FlowControlId = transaction.FlowControl?.Id,
                TransactionStatus = transaction.TransactionStatus + 1
            }
        );
        return rowsInserted == 1;
    }

    public bool Update(Transaction transaction)
    {
        throw new NotImplementedException();
    }
}