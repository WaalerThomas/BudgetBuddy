using BudgetBuddy.Account.Model;
using BudgetBuddy.Category.Model;
using BudgetBuddy.Client.Model;
using BudgetBuddy.Transaction.Model;
using Microsoft.EntityFrameworkCore;

namespace BudgetBuddy.Data;

public class DatabaseContext : DbContext
{
    public DbSet<ClientDao> Clients { get; set; }
    public DbSet<AccountDao> Accounts { get; set; }
    public DbSet<CategoryDao> Categories { get; set; }
    public DbSet<TransactionDao> Transactions { get; set; }

    private string DbPath { get; }

    public DatabaseContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "BudgetBuddy", "budgetbuddy.db");
    }
    
    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }
}