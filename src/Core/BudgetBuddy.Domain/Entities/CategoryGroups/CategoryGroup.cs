namespace BudgetBuddy.Domain.Entities.CategoryGroups;

public sealed class CategoryGroup
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public List<int> CategoryIds { get; set; } = [];
}