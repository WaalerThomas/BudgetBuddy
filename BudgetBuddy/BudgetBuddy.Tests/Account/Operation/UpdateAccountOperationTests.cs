using AutoMapper;
using BudgetBuddy.Account.AutoMapper;
using BudgetBuddy.Account.Model;
using BudgetBuddy.Account.Operations;
using BudgetBuddy.Account.Repositories;
using BudgetBuddy.Account.Resources;
using BudgetBuddy.Account.Service;
using BudgetBuddy.Core.Exceptions;
using BudgetBuddy.Tests.Common;
using NSubstitute;

namespace BudgetBuddy.Tests.Account.Operation;

[TestFixture]
public class UpdateAccountOperationTests
{
    private UpdateAccountOperation _operation;
    
    private IAccountRepository _accountRepository;
    private IAccountValidator _accountValidator;
    private IMapper _mapper;
    
    [SetUp]
    public void SetUp()
    {
        _accountRepository = Substitute.For<IAccountRepository>();
        _accountValidator = Substitute.For<IAccountValidator>();
        
        _mapper = new Mapper(new MapperConfiguration(
            cfg => cfg.AddProfile<AccountProfile>()));
        
        _operation = new UpdateAccountOperation(_accountRepository, _accountValidator, _mapper);
    }

    [Test]
    public void UpdateAccount_ShouldCallValidateNameUniqueness()
    {
        // arrange
        var accountModel = TestHelper.CreateAccount();
        var accountDao = new AccountDao { Id = accountModel.Id, Name = accountModel.Name };
        _accountRepository.GetById(Arg.Any<int>()).Returns(accountDao);
        
        // act
        _operation.Operate(accountModel);
        
        // assert
        _accountValidator.Received().ValidateNameUniqueness(Arg.Is(accountModel));
    }

    [Test]
    public void UpdateAccount_ShouldCallUpdateOnRepository()
    {
        // arrange
        var accountModel = TestHelper.CreateAccount();
        var accountDao = new AccountDao { Id = accountModel.Id, Name = accountModel.Name };
        _accountRepository.GetById(Arg.Any<int>()).Returns(accountDao);
        
        // act
        _operation.Operate(accountModel);
        
        // assert
        _accountRepository.Received().Update(Arg.Is<AccountDao>(x => x.Id == accountModel.Id));
    }
}