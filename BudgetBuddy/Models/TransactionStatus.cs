namespace BudgetBuddy.Models;

public class TransactionStatus
{
    public TransactionStatusEnum Id { get; set; }
    public required string Name { get; set; }
}

public enum TransactionStatusEnum { Settled, Pending, ReconciliationPoint }