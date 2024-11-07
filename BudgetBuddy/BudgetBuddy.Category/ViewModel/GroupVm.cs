namespace BudgetBuddy.Category.ViewModel;

public class GroupVm
{
    public int Id { get; set; }
    public Guid ClientId { get; set; }
    public string Name { get; set; } = string.Empty;
}