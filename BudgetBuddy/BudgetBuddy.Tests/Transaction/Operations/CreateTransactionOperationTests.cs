using AutoMapper;
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
        _mapper = Substitute.For<IMapper>();
        
        _operation = new CreateTransactionOperation(_transactionRepository, _transactionValidator, _mapper);
    }
}