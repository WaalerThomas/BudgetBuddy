using AutoMapper;
using BudgetBuddy.Contracts.Model.Transaction;
using BudgetBuddy.Core.Operation;
using BudgetBuddy.Transaction.Model;
using BudgetBuddy.Transaction.Repositories;
using BudgetBuddy.Transaction.Service;
using FluentValidation;

namespace BudgetBuddy.Transaction.Operations;

public class CreateTransactionOperation : Operation<TransactionModel, TransactionModel>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ITransactionValidator _transactionValidator;
    private readonly IMapper _mapper;

    public CreateTransactionOperation(
        ITransactionRepository transactionRepository,
        ITransactionValidator transactionValidator,
        IMapper mapper)
    {
        _transactionRepository = transactionRepository;
        _transactionValidator = transactionValidator;
        _mapper = mapper;
    }

    protected override TransactionModel OnOperate(TransactionModel transactionModel)
    {
        _transactionValidator.ValidateAndThrow(transactionModel);
        _transactionValidator.ValidateTransaction(transactionModel);
        
        var transactionDao = _mapper.Map<TransactionDao>(transactionModel);
        transactionDao = _transactionRepository.Create(transactionDao);
        
        transactionModel = _mapper.Map<TransactionModel>(transactionDao);
        return transactionModel;
    }
}