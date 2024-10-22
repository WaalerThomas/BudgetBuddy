using System.Security.Cryptography;
using BudgetBuddy.Controllers;
using BudgetBuddy.Models;

namespace BudgetBuddy.Test;

[TestClass]
public class AccountTest
{
    [TestMethod]
    public void CreateAccount_WithValidName_ReturnsAccount()
    {
        Account account = new() { Name = "Debit Card" };

        Assert.AreEqual("Debit Card", account.Name);
    }

    [TestMethod]
    public void CreateAccount_WithTooShortName_ThrowsException()
    {
        try
        {
            _ = new Account { Name = "Bad" };
        }
        catch (ArgumentException ex)
        {
            StringAssert.Contains(ex.Message, "Name is too short.");
            return;
        }

        Assert.Fail("The expected exception was not thrown");
    }

    [TestMethod]
    public void CreateAccount_WithTooLongName_ThrowsException()
    {
        try
        {
            _ = new Account() { Name = "This name is going to be way too long." };
        }
        catch (ArgumentException ex)
        {
            StringAssert.Contains(ex.Message, "Name is too long.");
            return;
        }

        Assert.Fail("The expected exception was not thrown");
    }

    [TestMethod]
    public void CreateAccount_NameAtMinLength_ReturnsAccount()
    {
        Account account = new() { Name = "Duck" };

        Assert.AreEqual("Duck", account.Name);
    }

    [TestMethod]
    public void CreateAccount_NameAtMaxLength_ReturnsAccount()
    {
        Account account = new() { Name = "Quack, said the duckling!" };

        Assert.AreEqual("Quack, said the duckling!", account.Name);
    }

    [TestMethod]
    public void Renaming_WithValidName_SetsNewName()
    {
        Account account = new() { Name = "Debit Card" };
        
        account.Name = "Cashing";

        Assert.AreEqual("Cashing", account.Name);
    }

    [TestMethod]
    public void Renaming_WithTooShortName_ThrowsException()
    {
        Account account = new() { Name = "Debit Card" };

        try
        {
            account.Name = "Bad";
        }
        catch (ArgumentException ex)
        {
            StringAssert.Contains(ex.Message, "Name is too short.");
            return;
        }

        Assert.Fail("The expected exception was not thrown");
    }

    [TestMethod]
    public void Renaming_WithTooLongName_ThrowsException()
    {
        Account account = new() { Name = "Debit Card" };

        try
        {
            account.Name = "This name is going to be way too long.";
        }
        catch (ArgumentException ex)
        {
            StringAssert.Contains(ex.Message, "Name is too long.");
            return;
        }

        Assert.Fail("The expected exception was not thrown");
    }
}