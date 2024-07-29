namespace BudgetBuddy.Models;

public class Transaction
{
    public int Id { get; set; }
    public required DateOnly EntryDate { get; set; }
    public decimal Amount { get; set; }
    public required TransactionStatus TransactionStatus { get; set; }
}

