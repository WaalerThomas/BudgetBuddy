using BudgetBuddy.Contracts.Model.Transaction;
using FluentValidation;

namespace BudgetBuddy.Transaction.Service;

public class TransactionValidator : AbstractValidator<TransactionModel>, ITransactionValidator
{
    public TransactionValidator()
    {
    }
}