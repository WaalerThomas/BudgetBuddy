using BudgetBuddy.Contracts.Enums;
using BudgetBuddy.Contracts.Model.Transaction;
using BudgetBuddy.Core.Exceptions;
using BudgetBuddy.Transaction.Resources;
using FluentValidation;

namespace BudgetBuddy.Transaction.Service;

public class TransactionValidator : AbstractValidator<TransactionModel>, ITransactionValidator
{
    public TransactionValidator()
    {
        RuleFor(x => x.Type).IsInEnum();
        RuleFor(x => x.Status).IsInEnum();
    }

    public void ValidateTransaction(TransactionModel transactionModel)
    {
        // TODO: How would we validate the date?
        
        switch (transactionModel.Type)
        {
            case TransactionType.Category:
                ValidateCategoryTransaction(transactionModel);
                break;
            case TransactionType.AccountTransfer:
                ValidateAccountTransferTransaction(transactionModel);
                break;
            case TransactionType.BalanceAdjustment:
                ValidateBalanceAdjustmentTransaction(transactionModel);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void ValidateCategoryTransaction(TransactionModel transactionModel)
    {
        const string typeErrorMessage = "category transactions";
        
        if (transactionModel.ToAccountId is not null)
        {
            throw new BuddyException(string.Format(
                TransactionResource.FieldIsNotAllowedForType, "To Account Id", typeErrorMessage));
        }
        
        if (transactionModel.CategoryId is null)
        {
            throw new BuddyException(string.Format(
                TransactionResource.FieldIsRequiredForType, "Category Id", typeErrorMessage));
        }
        
        if (transactionModel.FromAccountId is null)
        {
            throw new BuddyException(string.Format(
                TransactionResource.FieldIsRequiredForType, "From Account Id", typeErrorMessage));
        }

        if (transactionModel.Amount >= 0)
        {
            throw new BuddyException("Category transactions must have a negative amount");
        }
    }
    
    private void ValidateAccountTransferTransaction(TransactionModel transactionModel)
    {
        const string typeErrorMessage = "account transfer transactions";
        
        if (transactionModel.CategoryId is not null)
        {
            throw new BuddyException(string.Format(
                TransactionResource.FieldIsNotAllowedForType, "Category Id", typeErrorMessage));
        }
        
        if (transactionModel.FromAccountId is null)
        {
            throw new BuddyException(string.Format(
                TransactionResource.FieldIsRequiredForType, "From Account Id", typeErrorMessage));
        }
        
        if (transactionModel.ToAccountId is null)
        {
            throw new BuddyException(string.Format(
                TransactionResource.FieldIsRequiredForType, "To Account Id", typeErrorMessage));
        }
        
        if (transactionModel.Amount <= 0)
        {
            throw new BuddyException("Account transfer transactions must have a positive amount");
        }
    }
    
    private void ValidateBalanceAdjustmentTransaction(TransactionModel transactionModel)
    {
        const string typeErrorMessage = "balance adjustment transactions";
        
        if (transactionModel.CategoryId is not null)
        {
            throw new BuddyException(string.Format(
                TransactionResource.FieldIsNotAllowedForType, "Category Id", typeErrorMessage));
        }

        if (transactionModel.FromAccountId is not null)
        {
            throw new BuddyException(string.Format(
                TransactionResource.FieldIsNotAllowedForType, "From Account Id", typeErrorMessage));
        }
        
        if (transactionModel.ToAccountId is null)
        {
            throw new BuddyException(string.Format(
                TransactionResource.FieldIsRequiredForType, "To Account Id", typeErrorMessage));
        }

        if (transactionModel.Amount == 0)
        {
            throw new BuddyException("Balance adjustment transactions must have a non-zero amount");
        }
    }
}