namespace BudgetBuddy.Contracts.Model.Category;

public class GroupModel
{
    public int Id { get; set; }
    public Guid ClientId { get; set; }
    public string Name { get; set; } = string.Empty;
}