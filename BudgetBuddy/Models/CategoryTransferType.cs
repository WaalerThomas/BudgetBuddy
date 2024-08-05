namespace BudgetBuddy.Models;

public class CategoryTransferType
{
    public CategoryTransferTypeEnum Id { get; set; }
    public required string Name { get; set; }

    public ICollection<CategoryTransfer> CategoryTransfers { get; } = new List<CategoryTransfer>();
}

public enum CategoryTransferTypeEnum
{
    IntoAvailableToBudget,
    FromAvailableToBudget,
    FromCategory
}