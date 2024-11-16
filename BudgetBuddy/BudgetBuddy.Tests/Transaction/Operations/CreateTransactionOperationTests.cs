using AutoMapper;
using BudgetBuddy.Tests.Common;
using BudgetBuddy.Transaction.AutoMapper;
using BudgetBuddy.Transaction.Model;
using BudgetBuddy.Transaction.Operations;
using BudgetBuddy.Transaction.Repositories;
using BudgetBuddy.Transaction.Service;
using NSubstitute;

namespace BudgetBuddy.Tests.Transaction.Operations;

[TestFixture]
public class CreateTransactionOperationTests
{
    private CreateTransactionOperation _operation;
    
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
        
        _operation = new CreateTransactionOperation(_transactionRepository, _transactionValidator, _mapper);
    }

    [Test]
    public void CreateTransaction_ShouldCallCreateOnRepository()
    {
        // arrange
        var transactionModel = TestHelper.CreateCategoryTransaction();
        
        // act
        _operation.Operate(transactionModel);
        
        // assert
        _transactionRepository.Received().Create(Arg.Any<TransactionDao>());
    }

    [Test]
    public void CreateTransaction_ShouldCallValidateTransaction()
    {
        // arrange
        var transactionModel = TestHelper.CreateCategoryTransaction();
        
        // act
        _operation.Operate(transactionModel);
        
        // assert
        _transactionValidator.Received().ValidateTransaction(Arg.Is(transactionModel));
    }
}