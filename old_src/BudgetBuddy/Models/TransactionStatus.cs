namespace BudgetBuddy.Models;

public class TransactionStatus
{
    public TransactionStatusEnum Id { get; set; }
    public required string Name { get; set; }

    public ICollection<Transaction> Transactions { get; } = new List<Transaction>();
}

public enum TransactionStatusEnum
{
    Settled,
    Pending,
    ReconciliationPoint
}