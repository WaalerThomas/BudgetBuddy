using AutoMapper;
using BudgetBuddy.Contracts.Model.Transaction;
using BudgetBuddy.Core.Operation;
using BudgetBuddy.Transaction.Repositories;

namespace BudgetBuddy.Transaction.Operations;

public class GetTransactionByIdOperation : Operation<int, TransactionModel>
{
    private readonly IMapper _mapper;
    private readonly ITransactionRepository _transactionRepository;

    public GetTransactionByIdOperation(ITransactionRepository transactionRepository, IMapper mapper)
    {
        _transactionRepository = transactionRepository;
        _mapper = mapper;
    }

    protected override TransactionModel OnOperate(int id)
    {
        var transactionDao = _transactionRepository.GetById(id);
        return _mapper.Map<TransactionModel>(transactionDao);
    }
}