using BudgetBuddy.Models;
using BudgetBuddy.Repositories;

namespace BudgetBuddy.Sqlite.Repositories;

public class AccountRespository : Repository<Account>, IAccountRepository
{
    public DatabaseContext DatabaseContext => (DatabaseContext)Context;

    public AccountRespository(DatabaseContext context)
        : base(context)
    {
    }
}