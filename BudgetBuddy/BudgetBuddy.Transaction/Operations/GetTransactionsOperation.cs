using AutoMapper;
using BudgetBuddy.Contracts.Model.Transaction;
using BudgetBuddy.Core.Operation;
using BudgetBuddy.Transaction.Repositories;

namespace BudgetBuddy.Transaction.Operations;

public class GetTransactionsOperation : Operation<object, IEnumerable<TransactionModel>>
{
    private readonly IMapper _mapper;
    private readonly ITransactionRepository _transactionRepository;

    public GetTransactionsOperation(ITransactionRepository transactionRepository, IMapper mapper)
    {
        _transactionRepository = transactionRepository;
        _mapper = mapper;
    }

    protected override IEnumerable<TransactionModel> OnOperate(object request)
    {
        var transactions = _transactionRepository.GetAll();
        return _mapper.Map<IEnumerable<TransactionModel>>(transactions);
    }
}