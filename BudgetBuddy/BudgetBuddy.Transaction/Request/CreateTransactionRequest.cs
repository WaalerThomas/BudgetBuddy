using BudgetBuddy.Contracts.Enums;

namespace BudgetBuddy.Transaction.Request;

public class CreateTransactionRequest
{
    public DateOnly Date { get; set; }
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public int? CategoryId { get; set; }
    public int? FromAccountId { get; set; }
    public int? ToAccountId { get; set; }
    public string Memo { get; set; } = string.Empty;
    public TransactionStatus Status { get; set; }
}