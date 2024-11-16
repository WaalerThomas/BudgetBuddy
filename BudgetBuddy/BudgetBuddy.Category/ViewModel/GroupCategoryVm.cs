namespace BudgetBuddy.Category.ViewModel;

public class GroupCategoryVm
{
    public GroupVm Group { get; set; } = new();
    public IEnumerable<CategoryVm> Categories { get; set; } = new List<CategoryVm>();
}