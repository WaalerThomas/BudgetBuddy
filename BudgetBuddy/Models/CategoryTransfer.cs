namespace BudgetBuddy.Models;

public class CategoryTransfer
{
    public int Id { get; set; }
    public required DateOnly EntryDate { get; set; }
    public required decimal Amount { get; set; }
    public required Category? FromCategory { get; set; }
    public required Category? ToCategory { get; set; }
}