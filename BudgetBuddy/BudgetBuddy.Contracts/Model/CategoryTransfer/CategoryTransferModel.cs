using BudgetBuddy.Contracts.Model.Common;

namespace BudgetBuddy.Contracts.Model.CategoryTransfer;

public class CategoryTransferModel : TimeKeepModel
{
    public DateOnly Date { get; set; }
    public decimal Amount { get; set; }
    public int? FromCategoryId { get; set; }
    public int ToCategoryId { get; set; }
    public bool FromAvailableToBudget { get; set; }
}