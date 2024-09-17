using BudgetBuddy.Domain.Common;

namespace BudgetBuddy.Domain.Entities.CategoryGroups;

public static class CategoryGroupErrors
{
    public static readonly Error NameNotUnique = new("CategoryGroups.NameNotUnique", "The provided name is not unique");
}