using AutoMapper;
using BudgetBuddy.Contracts.Interface.Transaction;
using BudgetBuddy.Contracts.Model.Transaction;
using BudgetBuddy.Core.Exceptions;
using BudgetBuddy.Transaction.Controller;
using BudgetBuddy.Transaction.Request;
using BudgetBuddy.Transaction.Resources;
using NSubstitute;

namespace BudgetBuddy.Tests.Transaction;

[TestFixture]
public class TransactionControllerTests
{
    private TransactionController _controller;
    
    private ITransactionService _transactionService;
    private IMapper _mapper;
    
    [SetUp]
    public void SetUp()
    {
        _transactionService = Substitute.For<ITransactionService>();
        _mapper = Substitute.For<IMapper>();
        
        _controller = new TransactionController(_mapper, _transactionService);
    }
    
    # region Update Transaction
    
    [Test]
    public void Update_WhenTransactionNotFound_ThrowsException()
    {
        // assert
        var exception = Assert.Throws<BuddyException>(() => _controller.Update(new UpdateTransactionRequest()));
        Assert.That(exception.Message, Is.EqualTo(TransactionResource.TransactionNotFound));
    }
    
    [Test]
    public void Update_WhenTransactionFound_UpdatesTransaction()
    {
        // arrange
        var transactionModel = new TransactionModel();
        _transactionService.GetById(Arg.Any<int>()).Returns(transactionModel);
        
        // act
        _controller.Update(new UpdateTransactionRequest());
        
        // assert
        _transactionService.Received().Update(Arg.Any<TransactionModel>());
    }
    
    # endregion
}