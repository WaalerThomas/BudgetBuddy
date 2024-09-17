using BudgetBuddy.Models;
using BudgetBuddy.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BudgetBuddy.Sqlite.Repositories;

public class CategoryRepository(DbContext context)
    : Repository<Category>(context), ICategoryRepository
{
    public DatabaseContext DatabaseContext => (DatabaseContext)Context;

    public decimal GetAvailableAmount(int id)
    {
        var transactionFlow = DatabaseContext.Transactions
            .Where(t => t.TransactionType.Id == TransactionTypeEnum.Category)
            .Where(t => t.Category != null && t.Category.Id == id)
            .Select(t => t.Amount)
            .ToList().Sum();
        var transferFromFlow = DatabaseContext.CategoryTransfers
            .Where(t => t.TransferType.Id == CategoryTransferTypeEnum.FromCategory || t.TransferType.Id == CategoryTransferTypeEnum.IntoAvailableToBudget)
            .Where(t => t.FromCategory != null && t.FromCategory.Id == id)
            .Select(t => t.Amount)
            .ToList().Sum();
        var transferToFlow = DatabaseContext.CategoryTransfers
            .Where(t => t.TransferType.Id == CategoryTransferTypeEnum.FromCategory || t.TransferType.Id == CategoryTransferTypeEnum.FromAvailableToBudget)
            .Where(t => t.ToCategory != null && t.ToCategory.Id == id)
            .Select(t => t.Amount)
            .ToList().Sum();
        
        return transactionFlow + transferToFlow - transferFromFlow;
    }

    public decimal GetActivityAmount(int id)
    {
        // TODO: Double check that we are covering everything

        DateTime today = DateTime.Today;
        DateOnly startOfMonth = DateOnly.FromDateTime(  new DateTime(today.Year, today.Month, 1) );
        DateOnly endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

        var transactionFlow = DatabaseContext.Transactions
            .Where(t => t.TransactionType.Id == TransactionTypeEnum.AccountTransfer || t.TransactionType.Id == TransactionTypeEnum.Category)
            .Where(t => t.Account != null && t.Account.Id == id)
            .Where(t => t.EntryDate >= startOfMonth && t.EntryDate <= endOfMonth)
            .Select(t => t.Amount)
            .ToList().Sum();

        return 0;
    }

    public decimal GetBudgetetAmount(int id)
    {
        // TODO: Implement
        return 0;
    }
}