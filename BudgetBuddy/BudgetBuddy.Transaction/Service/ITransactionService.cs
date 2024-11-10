using BudgetBuddy.Contracts.Model.Transaction;

namespace BudgetBuddy.Transaction.Service;

public interface ITransactionService
{
    TransactionModel Create(TransactionModel transactionModel);
}