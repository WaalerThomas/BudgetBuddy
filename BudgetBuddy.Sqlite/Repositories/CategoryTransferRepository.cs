using BudgetBuddy.Models;
using BudgetBuddy.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BudgetBuddy.Sqlite.Repositories;

public class CategoryTransferRepository : Repository<CategoryTransfer>, ICategoryTransferRepository
{
    public DatabaseContext DatabaseContext => (DatabaseContext)Context;

    public CategoryTransferRepository(DbContext context)
        : base(context)
    {
    }

    public decimal GetCashFlowSum()
    {
        var transferIntoAtoB = DatabaseContext.CategoryTransfers
            .Where(t => t.TransferType.Id == CategoryTransferTypeEnum.IntoAvailableToBudget)
            .Select(t => t.Amount)
            .ToList().Sum();
        var transferFromAtoB = DatabaseContext.CategoryTransfers
            .Where(t => t.TransferType.Id == CategoryTransferTypeEnum.FromAvailableToBudget)
            .Select(t => t.Amount)
            .ToList().Sum();
        
        return transferIntoAtoB - transferFromAtoB;
    }

    public override void Add(CategoryTransfer transfer)
    {
        // Need to attach the type enum so that EF doesn't try to add into tables
        DatabaseContext.CategoryTransferTypes.Attach(transfer.TransferType);

        DatabaseContext.CategoryTransfers.Add(transfer);
    }

    public IEnumerable<CategoryTransfer> GetAllWithExtra()
    {
        return DatabaseContext.CategoryTransfers
            .Include(t => t.FromCategory)
            .Include(t => t.ToCategory)
            .Include(t => t.TransferType)
            .ToList();
    }
}