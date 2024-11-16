using AutoMapper;
using BudgetBuddy.Contracts.Enums;
using BudgetBuddy.Contracts.Model.Transaction;
using BudgetBuddy.Core.Exceptions;
using BudgetBuddy.Tests.Common;
using BudgetBuddy.Transaction.AutoMapper;
using BudgetBuddy.Transaction.Model;
using BudgetBuddy.Transaction.Operations;
using BudgetBuddy.Transaction.Repositories;
using BudgetBuddy.Transaction.Resources;
using BudgetBuddy.Transaction.Service;
using NSubstitute;

namespace BudgetBuddy.Tests.Transaction.Operations;

[TestFixture]
public class UpdateTransactionOperationTests
{
    private UpdateTransactionOperation _operation;
    
    private ITransactionRepository _transactionRepository;
    private ITransactionValidator _transactionValidator;
    private IMapper _mapper;
    
    [SetUp]
    public void SetUp()
    {
        _transactionRepository = Substitute.For<ITransactionRepository>();
        _transactionValidator = Substitute.For<ITransactionValidator>();
        _mapper = new Mapper(new MapperConfiguration(
            cfg => cfg.AddProfile<TransactionProfile>()));
        
        _operation = new UpdateTransactionOperation(_transactionRepository, _mapper, _transactionValidator);
    }

    [Test]
    public void UpdateTransaction_ShouldCallUpdateOnRepository()
    {
        // arrange
        _transactionRepository.GetById(Arg.Any<int>()).Returns(new TransactionDao { Type = TransactionType.Category });
        
        // act
        _operation.Operate(TestHelper.CreateCategoryTransaction());
        
        // assert
        _transactionRepository.Received().Update(Arg.Any<TransactionDao>());
    }

    [Test]
    public void UpdateTransaction_ShouldCallValidateTransaction()
    {
        // arrange
        _transactionRepository.GetById(Arg.Any<int>()).Returns(new TransactionDao { Type = TransactionType.Category });
        
        // act
        _operation.Operate(TestHelper.CreateCategoryTransaction());
        
        // assert
        _transactionValidator.Received().ValidateTransaction(Arg.Any<TransactionModel>());
    }
    
    [Test]
    public void UpdateTransaction_WhenTransactionNotFound_ShouldThrowsException()
    {
        // assert
        var exception = Assert.Throws<BuddyException>(
            () => _operation.Operate(TestHelper.CreateCategoryTransaction()));
        Assert.That(exception.Message, Is.EqualTo(TransactionResource.TransactionNotFound));
    }

    [Test]
    public void UpdateTransaction_WhenTypeChanged_ShouldThrowsException()
    {
        // arrange
        var transactionModel = TestHelper.CreateCategoryTransaction();
        _transactionRepository.GetById(Arg.Any<int>()).Returns(new TransactionDao { Type = TransactionType.AccountTransfer });
        
        // assert
        var exception = Assert.Throws<BuddyException>(
            () => _operation.Operate(transactionModel));
        Assert.That(exception.Message, Is.EqualTo(TransactionResource.TypeCannotChange));
    }
}