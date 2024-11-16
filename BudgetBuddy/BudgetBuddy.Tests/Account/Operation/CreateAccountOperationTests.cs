using AutoMapper;
using BudgetBuddy.Account.Model;
using BudgetBuddy.Account.Operations;
using BudgetBuddy.Account.Repositories;
using BudgetBuddy.Account.Service;
using BudgetBuddy.Contracts.Model.Account;
using BudgetBuddy.Tests.Common;
using NSubstitute;

namespace BudgetBuddy.Tests.Account.Operation;

[TestFixture]
public class CreateAccountOperationTests
{
    private CreateAccountOperation _operation;
    
    private IMapper _mapper;
    private IAccountRepository _accountRepository;
    private IAccountValidator _accountValidator;
    
    [SetUp]
    public void SetUp()
    {
        _mapper = Substitute.For<IMapper>();
        _accountRepository = Substitute.For<IAccountRepository>();
        _accountValidator = Substitute.For<IAccountValidator>();
        
        _operation = new CreateAccountOperation(_accountValidator, _accountRepository, _mapper);
    }
    
    [Test]
    public void CreateAccount_ShouldCallCreateOnRepository()
    {
        // act
        _operation.Operate(TestHelper.CreateAccount());
        
        // assert
        _accountRepository.Received().Create(Arg.Any<AccountDao>());
    }

    [Test]
    public void CreateAccount_ShouldCallValidateNameUniqueness()
    {
        // act
        var account = TestHelper.CreateAccount();
        _operation.Operate(account);
        
        // assert
        _accountValidator.Received().ValidateNameUniqueness(Arg.Is(account));
    }
}