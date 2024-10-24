using BudgetBuddy.Account.Repositories;
using BudgetBuddy.Account.Service;
using NSubstitute;

namespace BudgetBuddy.Tests.Account.Service;

public class AccountServiceTests
{
    private IAccountService _accountService;

    private IAccountRepository _accountRepository;
    
    [SetUp]
    public void Setup()
    {
        _accountRepository = Substitute.For<IAccountRepository>();
    }
    
    [TestCase]
    public async Task CreateAccount_WithGoodName_ShouldReturnNewAccount()
    {
        // // Arrange
        // _accountRepository.CreateAccount(Arg.Any<AccountModel>())
        //     .Returns(new AccountModel{ Name = "Test Account" });
        //
        // // Act
        // var result = await _accountService.CreateAccount("Test Account");
        //
        // // Assert
        // Assert.That(result, Is.Not.Null);
        // Assert.Multiple(() =>
        // {
        //     Assert.That(result.Id, Is.Not.EqualTo(Guid.Empty));
        //     Assert.That(result.Name, Is.EqualTo("Test Account"));
        // });
    }
}