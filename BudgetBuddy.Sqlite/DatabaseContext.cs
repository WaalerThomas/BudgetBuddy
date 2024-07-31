using BudgetBuddy.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetBuddy.Sqlite;

public class DatabaseContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public DbSet<TransactionStatus> TransactionStatuses { get; set; }
    public DbSet<TransactionType> TransactionTypes { get; set; }

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed database with all statuses for transactions
        foreach (var transactionStatus in Enum.GetValues(typeof(TransactionStatusEnum)).Cast<TransactionStatusEnum>())
        {
            modelBuilder.Entity<TransactionStatus>().HasData(new TransactionStatus()
            {
                Id = transactionStatus,
                Name = transactionStatus.ToString()
            });
        }

        // Seed database with all types for transactions
        foreach (var transactionType in Enum.GetValues(typeof(TransactionTypeEnum)).Cast<TransactionTypeEnum>())
        {
            modelBuilder.Entity<TransactionType>().HasData(new TransactionType()
            {
                Id = transactionType,
                Name = transactionType.ToString()
            });
        }

        base.OnModelCreating(modelBuilder);
    }
}