using BudgetBuddy.Core.Operation;
using BudgetBuddy.Transaction.Repositories;

namespace BudgetBuddy.Transaction.Operations;

public class GetFlowSumOperation : Operation<object, decimal>
{
    private readonly ITransactionRepository _transactionRepository;

    public GetFlowSumOperation(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    protected override decimal OnOperate(object request)
    {
        return _transactionRepository.GetFlowSum();
    }
}