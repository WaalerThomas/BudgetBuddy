using BudgetBuddy.Contracts.Enums;
using BudgetBuddy.Contracts.Model.Common;

namespace BudgetBuddy.Contracts.Model.Category;

public class CategoryModel : TimeKeepModel
{
    public int Id { get; set; }
    public Guid ClientId { get; set; }
    public required string Name { get; set; }
    public decimal? MonthlyAmount { get; set; }
    public decimal? GoalAmount { get; set; }
    public int? GroupId { get; set; }
    public CategoryType Type { get; set; }
    
    public bool IsGroup => Type == CategoryType.Group;
    public bool IsCategory => Type == CategoryType.Category;
}