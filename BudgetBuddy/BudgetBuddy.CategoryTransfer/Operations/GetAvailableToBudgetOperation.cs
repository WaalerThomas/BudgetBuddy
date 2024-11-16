using BudgetBuddy.Contracts.Interface.Transaction;
using BudgetBuddy.Core.Operation;
using CategoryTransfer.Repositories;

namespace CategoryTransfer.Operations;

public class GetAvailableToBudgetOperation : Operation<object, decimal>
{
    private readonly ITransactionService _transactionService;
    private readonly ICategoryTransferRepository _categoryTransferRepository;

    public GetAvailableToBudgetOperation(ITransactionService transactionService, ICategoryTransferRepository categoryTransferRepository)
    {
        _transactionService = transactionService;
        _categoryTransferRepository = categoryTransferRepository;
    }

    protected override decimal OnOperate(object request)
    {
        var transactionFlow = _transactionService.GetFlowSum();
        var categoryTransferFlow = _categoryTransferRepository.GetFlowSum();
        
        return transactionFlow - categoryTransferFlow;
    }
}