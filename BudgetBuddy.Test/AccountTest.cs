using BudgetBuddy.Controllers;
using BudgetBuddy.Models;

namespace BudgetBuddy.Test;

[TestClass]
public class AccountTest
{
    [TestMethod]
    public void CreateAccount_WithValidName_ReturnsAccount()
    {
        Account account = AccountController.CreateAccount("Debit Card");

        Assert.AreEqual("Debit Card", account.Name);
    }

    [TestMethod]
    public void CreateAccount_WithTooShortName_ThrowsException()
    {
        try
        {
            AccountController.CreateAccount("Bad");
        }
        catch (ArgumentException ex)
        {
            StringAssert.Contains(ex.Message, "Name is too short.");
        }
    }

    [TestMethod]
    public void CreateAccount_WithTooLongName_ThrowsException()
    {
        try
        {
            AccountController.CreateAccount("This name is going to be way too long.");
        }
        catch (ArgumentException ex)
        {
            StringAssert.Contains(ex.Message, "Name is too long.");
        }
    }

    [TestMethod]
    public void CreateAccount_NameAtMinLength_ReturnsAccount()
    {
        Account account = AccountController.CreateAccount("Duck");

        Assert.AreEqual("Duck", account.Name);
    }

    [TestMethod]
    public void CreateAccount_NameAtMaxLength_ReturnsAccount()
    {
        Account account = AccountController.CreateAccount("Quack, said the duckling!");

        Assert.AreEqual("Quack, said the duckling!", account.Name);
    }
}