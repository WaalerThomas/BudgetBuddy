using Dapper;
using BudgetBuddyCore.Interfaces;
using BudgetBuddyCore.Models;

namespace BudgetBuddySqlite;

public class AccountRespository : IAccountRespository
{
    private readonly DatabaseContext _context;

    public AccountRespository(DatabaseContext context)
    {
        _context = context;
    }

    public void CreateAccount(string name)
    {
        if (Exists(name))
            throw new ArgumentException("Account name is already in use", name);
    }

    public bool Exists(string name)
    {
        Account? result = _context.GetConnection().QuerySingleOrDefault<Account>("SELECT * FROM account WHERE name = @name", new { name });
        return result != null;
    }




    public bool Delete(int id)
    {
        int rowsDeleted = _context.GetConnection().Execute("DELETE FROM account WHERE id = @id", new { id });
        return rowsDeleted == 1;
    }

    public Account? GetAccount(int id)
    {
        return _context.GetConnection().QuerySingleOrDefault<Account>("SELECT * FROM account WHERE id = @id", new { id });
    }

    public Account? GetAccountByName(string name)
    {
        return _context.GetConnection().QuerySingleOrDefault<Account>("SELECT * FROM account WHERE name = @name", new { name });
    }

    public IEnumerable<Account> GetAccounts()
    {
        return _context.GetConnection().Query<Account>("SELECT * FROM account");
    }

    public bool Insert(Account account)
    {
        int rowsInserted = _context.GetConnection().Execute(
            @"
                INSERT INTO account (name, balance)
                VALUES (@Name, @Balance)
            ",
            account
        );

        return rowsInserted == 1;
    }

    public bool Update(Account account)
    {
        int rowsUpdated = _context.GetConnection().Execute(
            @"
                UPDATE account
                SET name = @Name, balance = @Balance
                WHERE id = @Id
            ",
            account
        );

        return rowsUpdated == 1;
    }
}