using BudgetBuddy.Contracts.Model.Transaction;
using BudgetBuddy.Core.Operation;

namespace BudgetBuddy.Transaction.Operations;

public class CreateTransactionOperation : Operation<TransactionModel, TransactionModel>
{
    protected override TransactionModel OnOperate(TransactionModel request)
    {
        throw new NotImplementedException();
    }
}