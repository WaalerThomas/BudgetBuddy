using BudgetBuddy.Contracts.Model.Transaction;
using BudgetBuddy.Core.Operation;
using BudgetBuddy.Core.Service;
using BudgetBuddy.Transaction.Operations;

namespace BudgetBuddy.Transaction.Service;

public class TransactionService : ServiceBase, ITransactionService
{
    public TransactionService(IOperationFactory operationFactory) : base(operationFactory)
    {
    }

    public TransactionModel Create(TransactionModel transactionModel)
    {
        var operation = CreateOperation<CreateTransactionOperation>();
        return operation.Operate(transactionModel);
    }
}