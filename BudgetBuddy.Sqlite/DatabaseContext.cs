using BudgetBuddy.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetBuddy.Sqlite;

public class DatabaseContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }

    public string DbPath { get; }

    public DatabaseContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "BudgetBuddy", "budgetbuddy.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite($"Data Source={DbPath}");
}