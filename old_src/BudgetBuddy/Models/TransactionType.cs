namespace BudgetBuddy.Models;

public class TransactionType
{
    public TransactionTypeEnum Id { get; set; }
    public required string Name { get; set; }

    public ICollection<Transaction> Transactions { get; } = new List<Transaction>();
}

public enum TransactionTypeEnum
{
    Category,           // NOTE: Is this even a good name for this type?
    AccountTransfer,
    BalanceAdjustment,
}