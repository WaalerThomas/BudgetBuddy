using BudgetBuddyCore.Interfaces;

namespace BudgetBuddyCore;

public class Application
{
    public IAccountRespository AccountRepository { get; }
    public IGroupRepository GroupRepository { get; }
    public ICategoryRepository CategoryRepository { get; }
    public ITransactionRepository TransactionRepository { get; }

    //decimal? availableToBudget;

    public Application(
        IAccountRespository accountRespository,
        IGroupRepository groupRepository,
        ICategoryRepository categoryRepository,
        ITransactionRepository transactionRepository
    )
    {
        AccountRepository = accountRespository;
        GroupRepository = groupRepository;
        CategoryRepository = categoryRepository;
        TransactionRepository = transactionRepository;
    }
}