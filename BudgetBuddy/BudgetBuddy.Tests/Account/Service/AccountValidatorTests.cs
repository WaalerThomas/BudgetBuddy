using BudgetBuddy.Account.Model;
using BudgetBuddy.Account.Repositories;
using BudgetBuddy.Account.Resources;
using BudgetBuddy.Account.Service;
using BudgetBuddy.Core.Exceptions;
using BudgetBuddy.Tests.Common;
using NSubstitute;

namespace BudgetBuddy.Tests.Account.Service;

[TestFixture]
public class AccountValidatorTests
{
    private AccountValidator _validator;
    private IAccountRepository _accountRepository;

    [SetUp]
    public void Setup()
    {
        _accountRepository = Substitute.For<IAccountRepository>();
        _validator = new AccountValidator(_accountRepository);
    }
    
    [Test]
    public void Validate_WhenNameIsEmpty_ShouldReturnErrorMessage()
    {
        // arrange
        var account = TestHelper.CreateAccount(name: string.Empty);
        
        // act
        var result = _validator.Validate(account);
        
        // assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo(AccountResource.NameRequired));
        });
    }
    
    [Test]
    public void Validate_WhenTypeIsNotSet_ShouldReturnErrorMessage()
    {
        // arrange
        var account = TestHelper.CreateAccount(type: 0);
        
        // act
        var result = _validator.Validate(account);
        
        // assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo(AccountResource.AccountTypeRequired));
        });
    }

    #region ValidateNameUniqueness
    
    [Test]
    public void ValidateNameUniqueness_WhenNameIsNotUnique_ShouldThrowException()
    {
        // arrange
        var accountModel = TestHelper.CreateAccount(name: "Account", id: 4);
        _accountRepository.GetByName(accountModel.Name).Returns(new AccountDao { Id = 3, Name = "Account"});
        
        // assert
        var exception = Assert.Throws<BuddyException>(
            () => _validator.ValidateNameUniqueness(accountModel));
        Assert.That(exception.Message, Is.EqualTo(AccountResource.AccountNameNotUnique));
    }
    
    [Test]
    public void ValidateNameUniqueness_WhenNameIsUnique_ShouldNotThrowException()
    {
        // arrange
        var accountModel = TestHelper.CreateAccount(name: "Account", id: 4);
        _accountRepository.GetByName(accountModel.Name).Returns((AccountDao?)null);
        
        // act
        _validator.ValidateNameUniqueness(accountModel);
    }
    
    #endregion
}