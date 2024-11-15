using BudgetBuddy.Core.Database;

namespace CategoryTransfer.Model;

public class CategoryTransferDao : BuddyDao
{
    public DateOnly Date { get; set; }
    public decimal Amount { get; set; }
    public int? FromCategoryId { get; set; }
    public int ToCategoryId { get; set; }
    public bool FromAvailableToBudget { get; set; }
}