namespace BudgetBuddy.Contracts.Model.Category;

public class GroupCategoryModel
{
    public GroupModel Group { get; set; } = new();
    public IEnumerable<CategoryModel> Categories { get; set; } = new List<CategoryModel>();
}