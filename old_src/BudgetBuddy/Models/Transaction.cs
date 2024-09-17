namespace BudgetBuddy.Models;

public class Transaction
{
    public int Id { get; set; }
    public required DateOnly EntryDate { get; set; }
    public required TransactionType TransactionType { get; set; }
    public decimal Amount { get; set; }
    public Category? Category { get; set; }
    public Account? Account { get; set; }
    public required TransactionStatus TransactionStatus { get; set; }
}