using BudgetBuddy.Contracts.Model.Account;
using BudgetBuddy.Core.Operation;
using BudgetBuddy.Core.Request.Common;
using BudgetBuddy.Transaction.Repositories;
using BudgetBuddy.Transaction.Request;

namespace BudgetBuddy.Transaction.Operations;

public class GetBalanceOperation : Operation<GetBalanceRequest, AccountBalanceModel>
{
    private readonly ITransactionRepository _transactionRepository;

    public GetBalanceOperation(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    protected override AccountBalanceModel OnOperate(GetBalanceRequest request)
    {
        var balance = _transactionRepository.GetBalance(request.Id, request.OnlyActualBalance);
        return balance;
    }
}