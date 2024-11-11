using AutoMapper;
using BudgetBuddy.Contracts.Model.Transaction;
using BudgetBuddy.Core.Exceptions;
using BudgetBuddy.Core.Operation;
using BudgetBuddy.Transaction.Model;
using BudgetBuddy.Transaction.Repositories;
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

        var transactionDao = _transactionRepository.GetById(transactionModel.Id);
        if (transactionDao == null)
        {
            throw new BuddyException("Transaction not found");
        }
        
        if (transactionModel.Type != transactionDao.Type)
        {
            throw new BuddyException("Transaction type cannot be changed");
        }

        transactionDao = _mapper.Map<TransactionDao>(transactionModel);
        var updatedTransaction = _transactionRepository.Update(transactionDao);
        
        return _mapper.Map<TransactionModel>(updatedTransaction);
    }
}