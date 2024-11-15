using BudgetBuddy.Contracts.Interface.Transaction;
using BudgetBuddy.Contracts.Model.Account;
using BudgetBuddy.Contracts.Model.Transaction;
using BudgetBuddy.Core.Operation;
using BudgetBuddy.Core.Request.Common;
using BudgetBuddy.Core.Service;
using BudgetBuddy.Transaction.Operations;
using BudgetBuddy.Transaction.Request;

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

    public IEnumerable<TransactionModel> Get()
    {
        var operation = CreateOperation<GetTransactionsOperation>();
        return operation.Operate();
    }

    public TransactionModel? GetById(int id)
    {
        var operation = CreateOperation<GetTransactionByIdOperation>();
        return operation.Operate(id);
    }

    public TransactionModel Update(TransactionModel transactionModel)
    {
        var operation = CreateOperation<UpdateTransactionOperation>();
        return operation.Operate(transactionModel);
    }

    public AccountBalanceModel GetBalance(int id, bool onlyActualBalance = false)
    {
        var operation = CreateOperation<GetBalanceOperation>();
        return operation.Operate(new GetBalanceRequest { Id = id, OnlyActualBalance = onlyActualBalance });;
    }

    public decimal GetFlowSum()
    {
        var operation = CreateOperation<GetFlowSumOperation>();
        return operation.Operate();
    }
}