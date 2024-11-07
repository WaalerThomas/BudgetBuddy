namespace BudgetBuddy.Category.ViewModel;

public class GroupedCategoriesVm
{
    public IEnumerable<GroupCategoryVm> Groups { get; set; } = new List<GroupCategoryVm>();
}