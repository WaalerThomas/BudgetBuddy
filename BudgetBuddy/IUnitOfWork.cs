using BudgetBuddy.Repositories;

namespace BudgetBuddy;

public interface IUnitOfWork : IDisposable
{
    IAccountRepository Accounts { get; }
    IGroupRepository Groups { get; }
    ICategoryRepository Categories { get; }
    ITransactionRepository Transactions { get; }
    int Complete();
}