using BudgetBuddy.Models;

namespace BudgetBuddy.Controllers;

public class TransactionController
{
    public static TransactionType TypeFromEnum(TransactionTypeEnum type)
    {
        return new TransactionType()
        {
            Id = type,
            Name = type.ToString()
        };
    }

    public static TransactionStatus StatusFromEnum(TransactionStatusEnum status)
    {
        return new TransactionStatus()
        {
            Id = status,
            Name = status.ToString()
        };
    }

    // TODO: Make up a better name, it is way too long
    public static Transaction CreateAdjustingAccountTransaction(Account account, decimal amount)
    {
        // NOTE: The settled and pending balance isn't "controlled" by the transactions, but rather the transactions
        //       are there for "history" and the ability to "undo" the change if ever needed.
        account.SettledBalance += amount;

        return new Transaction()
        {
            EntryDate = DateTime.Now,
            TransactionType = TypeFromEnum(TransactionTypeEnum.BalanceAdjustment),
            Amount = amount,
            Account = account,
            TransactionStatus = StatusFromEnum(TransactionStatusEnum.Settled),
        };
    }
}