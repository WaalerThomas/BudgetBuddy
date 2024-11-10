using BudgetBuddy.Contracts.Model.Transaction;
using FluentValidation;

namespace BudgetBuddy.Transaction.Service;

public interface ITransactionValidator : IValidator<TransactionModel>
{
    public void ValidateTransaction(TransactionModel transactionModel);
}