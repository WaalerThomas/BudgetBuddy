using BudgetBuddy.Contracts.Enums;
using BudgetBuddy.Contracts.Model.Common;

namespace BudgetBuddy.Contracts.Model.Transaction;

public class TransactionModel : TimeKeepModel
{
    public int Id { get; set; }
    public Guid ClientId { get; set; }
    public DateOnly Date { get; set; }
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public int? CategoryId { get; set; }
    public int? AccountId { get; set; }
    public string Memo { get; set; } = string.Empty;
    public TransactionStatus Status { get; set; }
}