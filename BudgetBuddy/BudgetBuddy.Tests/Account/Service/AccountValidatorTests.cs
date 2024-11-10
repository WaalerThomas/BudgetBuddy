using BudgetBuddy.Account.Resources;
using BudgetBuddy.Account.Service;
using BudgetBuddy.Contracts.Interface.Common;
using BudgetBuddy.Tests.Common;
using NSubstitute;

namespace BudgetBuddy.Tests.Account.Service;

[TestFixture]
public class AccountValidatorTests
{
    private AccountValidator _validator;
    private ICommonValidators _commonValidators;

    [SetUp]
    public void Setup()
    {
        _commonValidators = Substitute.For<ICommonValidators>();
        
        _validator = new AccountValidator(_commonValidators);
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
}