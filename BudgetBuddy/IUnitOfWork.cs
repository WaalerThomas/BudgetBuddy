using BudgetBuddy.Repositories;

namespace BudgetBuddy;

public interface IUnitOfWork : IDisposable
{
    IAccountRepository Accounts { get; }
    int Complete();
}