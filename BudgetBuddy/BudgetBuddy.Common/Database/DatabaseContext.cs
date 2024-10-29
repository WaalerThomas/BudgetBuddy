using BudgetBuddy.Contracts.Model.Account;
using BudgetBuddy.Contracts.Model.Client;
using Microsoft.EntityFrameworkCore;

namespace BudgetBuddy.Common.Database;

public class DatabaseContext : DbContext
{
    public DbSet<ClientModel> Clients { get; set; }
    public DbSet<AccountModel> Accounts { get; set; }

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