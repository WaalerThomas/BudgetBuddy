using BudgetBuddy.Contracts.Model.Category;

namespace BudgetBuddy.Tests.Common;

public abstract class TestHelper
{
    public static CategoryModel CreateCategoryGroup(
        int id = 1,
        string clientId = "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        string name = "Expenses")
    {
        return new CategoryModel
        {
            Id = id,
            ClientId = Guid.Parse(clientId),
            Name = name,
            IsGroup = true
        };
    }
    
    public static CategoryModel CreateCategory(
        int id = 2,
        string clientId = "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        decimal monthlyAmount = 20,
        decimal? goalAmount = null,
        string name = "Groceries",
        int? groupId = 1)
    {
        return new CategoryModel
        {
            Id = id,
            ClientId = Guid.Parse(clientId),
            Name = name,
            MonthlyAmount = monthlyAmount,
            GoalAmount = goalAmount,
            IsGroup = false,
            GroupId = groupId
        };
    }
}