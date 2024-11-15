using BudgetBuddy.Contracts.Enums;
using BudgetBuddy.Contracts.Model.Transaction;
using BudgetBuddy.Core.Exceptions;
using BudgetBuddy.Transaction.Resources;
using BudgetBuddy.Transaction.Service;

namespace BudgetBuddy.Tests.Transaction.Service;

[TestFixture]
public class TransactionValidatorTests
{
    private TransactionValidator _validator;
    
    [SetUp]
    public void SetUp()
    {
        _validator = new TransactionValidator();
    }

    [Test]
    public void Validate_WithTypeOutOfEnum_ShouldThrowException()
    {
        // arrange
        var transaction = new TransactionModel { Type = (TransactionType) 5 };
        
        // act
        var result = _validator.Validate(transaction);
        
        // assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo(TransactionResource.TypeIsRequired));
        });
    }
    
    [Test]
    public void Validate_WithStatusOutOfEnum_ShouldThrowException()
    {
        // arrange
        var transaction = new TransactionModel { Type = TransactionType.Category, Status = (TransactionStatus) 5 };
        
        // act
        var result = _validator.Validate(transaction);
        
        // assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo(TransactionResource.StatusIsRequired));
        });
    }
    
    #region Validate Category Transaction
    
    [Test]
    public void ValidateCategoryTransaction_WithToAccountId_ShouldThrowException()
    {
        // arrange
        var transaction = new TransactionModel
        {
            Type = TransactionType.Category,
            ToAccountId = 1
        };
        
        // assert
        Assert.Throws<BuddyException>(
            () => _validator.ValidateTransaction(transaction),
            string.Format(TransactionResource.FieldIsNotAllowedForType, "To Account Id", "category transactions"));
    }
    
    [Test]
    public void ValidateCategoryTransaction_WithNoCategoryId_ShouldThrowException()
    {
        // arrange
        var transaction = new TransactionModel
        {
            Type = TransactionType.Category,
            CategoryId = null
        };
        
        // assert
        Assert.Throws<BuddyException>(
            () => _validator.ValidateTransaction(transaction),
            string.Format(TransactionResource.FieldIsRequiredForType, "Category Id", "category transactions"));
    }
    
    [Test]
    public void ValidateCategoryTransaction_WithNoFromAccountId_ShouldThrowException()
    {
        // arrange
        var transaction = new TransactionModel
        {
            Type = TransactionType.Category,
            FromAccountId = null
        };
        
        // assert
        Assert.Throws<BuddyException>(
            () => _validator.ValidateTransaction(transaction),
            string.Format(TransactionResource.FieldIsRequiredForType, "From Account Id", "category transactions"));
    }
    
    [Test]
    public void ValidateCategoryTransaction_WithPositiveAmount_ShouldThrowException()
    {
        // arrange
        var transaction = new TransactionModel
        {
            Type = TransactionType.Category,
            Amount = 1
        };
        
        // assert
        Assert.Throws<BuddyException>(
            () => _validator.ValidateTransaction(transaction),
            TransactionResource.CategoryTransactionNeedNegativeAmount);
    }
    
    #endregion
    
    #region Validate Account Transfer Transaction

    [Test]
    public void ValidateAccountTransferTransaction_WithCategoryId_ShouldThrowException()
    {
        // arrange
        var transaction = new TransactionModel
        {
            Type = TransactionType.AccountTransfer,
            CategoryId = 1
        };
        
        // assert
        Assert.Throws<BuddyException>(
            () => _validator.ValidateTransaction(transaction),
            string.Format(TransactionResource.FieldIsNotAllowedForType, "Category Id", "account transfer transactions"));
    }
    
    [Test]
    public void ValidateAccountTransferTransaction_WithNoFromAccountId_ShouldThrowException()
    {
        // arrange
        var transaction = new TransactionModel
        {
            Type = TransactionType.AccountTransfer,
            FromAccountId = null
        };
        
        // assert
        Assert.Throws<BuddyException>(
            () => _validator.ValidateTransaction(transaction),
            string.Format(TransactionResource.FieldIsRequiredForType, "From Account Id", "account transfer transactions"));
    }
    
    [Test]
    public void ValidateAccountTransferTransaction_WithNoToAccountId_ShouldThrowException()
    {
        // arrange
        var transaction = new TransactionModel
        {
            Type = TransactionType.AccountTransfer,
            ToAccountId = null
        };
        
        // assert
        Assert.Throws<BuddyException>(
            () => _validator.ValidateTransaction(transaction),
            string.Format(TransactionResource.FieldIsRequiredForType, "To Account Id", "account transfer transactions"));
    }
    
    [Test]
    public void ValidateAccountTransferTransaction_WithNegativeAmount_ShouldThrowException()
    {
        // arrange
        var transaction = new TransactionModel
        {
            Type = TransactionType.AccountTransfer,
            Amount = -1
        };
        
        // assert
        Assert.Throws<BuddyException>(
            () => _validator.ValidateTransaction(transaction),
            TransactionResource.AccountTransferTransactionNeedPositiveAmount);
    }
    
    #endregion

    #region Validate Balance Adjustment Transaction

    [Test]
    public void ValidateBalanceAdjustmentTransaction_WithCategoryId_ShouldThrowException()
    {
        // arrange
        var transaction = new TransactionModel
        {
            Type = TransactionType.BalanceAdjustment,
            CategoryId = 1
        };
        
        // assert
        Assert.Throws<BuddyException>(
            () => _validator.ValidateTransaction(transaction),
            string.Format(TransactionResource.FieldIsNotAllowedForType, "Category Id", "balance adjustment transactions"));
    }
    
    [Test]
    public void ValidateBalanceAdjustmentTransaction_WithNoFromAccountId_ShouldThrowException()
    {
        // arrange
        var transaction = new TransactionModel
        {
            Type = TransactionType.BalanceAdjustment,
            FromAccountId = null
        };
        
        // assert
        Assert.Throws<BuddyException>(
            () => _validator.ValidateTransaction(transaction),
            string.Format(TransactionResource.FieldIsRequiredForType, "From Account Id", "balance adjustment transactions"));
    }
    
    [Test]
    public void ValidateBalanceAdjustmentTransaction_WithToAccountId_ShouldThrowException()
    {
        // arrange
        var transaction = new TransactionModel
        {
            Type = TransactionType.BalanceAdjustment,
            ToAccountId = 1
        };
        
        // assert
        Assert.Throws<BuddyException>(
            () => _validator.ValidateTransaction(transaction),
            string.Format(TransactionResource.FieldIsNotAllowedForType, "To Account Id", "balance adjustment transactions"));
    }
    
    [Test]
    public void ValidateBalanceAdjustmentTransaction_WithZeroAmount_ShouldThrowException()
    {
        // arrange
        var transaction = new TransactionModel
        {
            Type = TransactionType.BalanceAdjustment,
            Amount = 0
        };
        
        // assert
        Assert.Throws<BuddyException>(
            () => _validator.ValidateTransaction(transaction),
            TransactionResource.BalanceAdjustmentTransactionNeedNonZeroAmount);
    }

    #endregion
}