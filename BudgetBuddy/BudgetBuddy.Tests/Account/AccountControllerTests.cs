using AutoMapper;
using BudgetBuddy.Account.AutoMapper;
using BudgetBuddy.Account.Controller;
using BudgetBuddy.Account.Request;
using BudgetBuddy.Account.Resources;
using BudgetBuddy.Contracts.Interface.Account;
using BudgetBuddy.Contracts.Model.Account;
using BudgetBuddy.Core.Exceptions;
using BudgetBuddy.Tests.Common;
using NSubstitute;

namespace BudgetBuddy.Tests.Account;

[TestFixture]
public class AccountControllerTests
{
    private AccountController _controller;
    
    private IAccountService _accountService;
    private IMapper _mapper;
    
    [SetUp]
    public void SetUp()
    {
        _accountService = Substitute.For<IAccountService>();
        _mapper = new Mapper(new MapperConfiguration(
            cfg => cfg.AddProfile<AccountProfile>()));
        
        _controller = new AccountController(_accountService, _mapper);
    }
    
    # region Update Account
    
    [Test]
    public void Update_WhenAccountNotFound_ThrowsException()
    {
        // assert
        var exception = Assert.Throws<BuddyException>(
            () => _controller.Update(new UpdateAccountRequest()));
        Assert.That(exception.Message, Is.EqualTo(AccountResource.AccountNotFound));
    }
    
    [Test]
    public void Update_WhenAccountFound_UpdatesAccount()
    {
        // arrange
        var accountModel = TestHelper.CreateAccount();
        _accountService.Get(Arg.Any<int>()).Returns(accountModel);
        
        // act
        _controller.Update(new UpdateAccountRequest());
        
        // assert
        _accountService.Received().Update(Arg.Any<AccountModel>());
    }
    
    # endregion
}