using AutoMapper;
using BudgetBuddy.Contracts.Model.Transaction;
using BudgetBuddy.Core.Exceptions;
using BudgetBuddy.Core.Operation;
using BudgetBuddy.Transaction.Model;
using BudgetBuddy.Transaction.Repositories;
using BudgetBuddy.Transaction.Resources;
using BudgetBuddy.Transaction.Service;
using FluentValidation;

namespace BudgetBuddy.Transaction.Operations;

public class UpdateTransactionOperation : Operation<TransactionModel, TransactionModel>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ITransactionValidator _transactionValidator;
    private readonly IMapper _mapper;

    public UpdateTransactionOperation(
        ITransactionRepository transactionRepository,
        IMapper mapper,
        ITransactionValidator transactionValidator)
    {
        _transactionRepository = transactionRepository;
        _mapper = mapper;
        _transactionValidator = transactionValidator;
    }

    protected override TransactionModel OnOperate(TransactionModel transactionModel)
    {
        _transactionValidator.ValidateAndThrow(transactionModel);
        _transactionValidator.ValidateTransaction(transactionModel);

        var existingTransactionDao = _transactionRepository.GetById(transactionModel.Id);
        if (existingTransactionDao is null)
        {
            throw new BuddyException(TransactionResource.TransactionNotFound);
        }
        
        if (transactionModel.Type != existingTransactionDao.Type)
        {
            throw new BuddyException(TransactionResource.TypeCannotChange);
        }

        var transactionDao = _mapper.Map<TransactionDao>(transactionModel);
        transactionDao = _transactionRepository.Update(transactionDao);
        
        return _mapper.Map<TransactionModel>(transactionDao);
    }
}