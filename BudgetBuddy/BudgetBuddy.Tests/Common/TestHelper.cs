using BudgetBuddy.Contracts.Enums;
using BudgetBuddy.Contracts.Model.Account;
using BudgetBuddy.Contracts.Model.Category;

namespace BudgetBuddy.Tests.Common;

public abstract class TestHelper
{
    public static AccountModel CreateAccount(
        int id = 1,
        string clientId = "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        string name = "Cash",
        string description = "Cash in my wallet",
        AccountType type = AccountType.BankOrCash)
    {
        return new AccountModel
        {
            Id = id,
            ClientId = Guid.Parse(clientId),
            Name = name,
            Description = description,
            Type = type
        };
    }
    
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
            Type = CategoryType.Group
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
            Type = CategoryType.Category, 
            GroupId = groupId
        };
    }
}