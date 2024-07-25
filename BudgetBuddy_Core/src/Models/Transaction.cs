namespace BudgetBuddyCore.Models;

public class Transaction
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public required Category Category { get; set; }
    public required Account Account { get; set; }
    public string? Memo { get; set; }
    public FlowControl? FlowControl { get; set; }
    public required TransactionStatus TransactionStatus { get; set; }

    public override string ToString()
    {
        return $"{Date} {Amount:C} Category: {Category} Account: {Account}";
    }
}

public enum TransactionStatus
{
    Completed,
    Pending,
    Reconciled
}