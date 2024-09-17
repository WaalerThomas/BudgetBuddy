using BudgetBuddy.Domain.Common;

namespace BudgetBuddy.Domain.Entities.Categories;

public static class CategoryErrors
{
    public static readonly Error NameNotUnique = new("Category.NameNotUnique", "The provided name is not unique");
    public static readonly Error MonthlyAmountNegative = new("Category.MonthlyAmountNegative", "The proviced monthly amount can not be negative");
    public static readonly Error GoalAmountNegative = new("Category.GoalAmountNegative", "The provided goal amount can not be negative");
}