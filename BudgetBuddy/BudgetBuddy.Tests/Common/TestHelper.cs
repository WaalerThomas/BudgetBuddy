using BudgetBuddy.Contracts.Enums;
using BudgetBuddy.Contracts.Model.Account;
using BudgetBuddy.Contracts.Model.Category;
using BudgetBuddy.Contracts.Model.Transaction;

namespace BudgetBuddy.Tests.Common;

public abstract class TestHelper
{
    private static readonly DateOnly DefaultDate = DateOnly.Parse("2021-01-01");

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

    public static TransactionModel CreateCategoryTransaction(
        int id = 1,
        string clientId = "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        DateOnly? date = null,
        TransactionType type = TransactionType.Category,
        decimal amount = -20,
        int? categoryId = 2,
        int? fromAccountId = 1,
        int? toAccountId = null,
        string memo = "Groceries",
        TransactionStatus status = TransactionStatus.Settled)
    {
        date ??= DefaultDate;
        
        return new TransactionModel
        {
            Id = id,
            ClientId = Guid.Parse(clientId),
            Date = date.Value,
            Type = type,
            Amount = amount,
            CategoryId = categoryId,
            FromAccountId = fromAccountId,
            ToAccountId = toAccountId,
            Memo = memo,
            Status = status
        };
    }
    
    public static TransactionModel CreateAccountTransferModel(
        int id = 1,
        string clientId = "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        DateOnly? date = null,
        TransactionType type = TransactionType.AccountTransfer,
        decimal amount = 200,
        int? categoryId = null,
        int? fromAccountId = 1,
        int? toAccountId = 2,
        string memo = "Transfer",
        TransactionStatus status = TransactionStatus.Settled)
    {
        date ??= DefaultDate;
        
        return new TransactionModel
        {
            Id = id,
            ClientId = Guid.Parse(clientId),
            Date = date.Value,
            Type = type,
            Amount = amount,
            CategoryId = categoryId,
            FromAccountId = fromAccountId,
            ToAccountId = toAccountId,
            Memo = memo,
            Status = status
        };
    }
    
    public static TransactionModel CreateBalanceAdjustmentTransaction(
        int id = 1,
        string clientId = "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        DateOnly? date = null,
        TransactionType type = TransactionType.BalanceAdjustment,
        decimal amount = 200,
        int? categoryId = null,
        int? fromAccountId = null,
        int? toAccountId = 1,
        string memo = "Adjustment",
        TransactionStatus status = TransactionStatus.Settled)
    {
        date ??= DefaultDate;
        
        return new TransactionModel
        {
            Id = id,
            ClientId = Guid.Parse(clientId),
            Date = date.Value,
            Type = type,
            Amount = amount,
            CategoryId = categoryId,
            FromAccountId = fromAccountId,
            ToAccountId = toAccountId,
            Memo = memo,
            Status = status
        };
    }
}