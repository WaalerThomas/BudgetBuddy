using BudgetBuddy.Controllers;
using BudgetBuddy.Models;

namespace BudgetBuddy.Test;

[TestClass]
public class GroupTest
{
    [TestMethod]
    public void CreateGroup_WithValidName_ReturnsGroup()
    {
        Group group = new() { Name = "Expenses" };

        Assert.AreEqual("Expenses", group.Name);
    }

    [TestMethod]
    public void CreateGroup_WithTooShortName_ThrowsException()
    {
        try
        {
            _ = new Group { Name = "Bad" };
        }
        catch (ArgumentException ex)
        {
            StringAssert.Contains(ex.Message, "Name is too short.");
            return;
        }

        Assert.Fail("The expected exception was not thrown");
    }

    [TestMethod]
    public void CreateGroup_WithTooLongName_ThrowsException()
    {
        try
        {
            _ = new Group { Name = "This name is going to be way too long." };
        }
        catch (ArgumentException ex)
        {
            StringAssert.Contains(ex.Message, "Name is too long.");
            return;
        }

        Assert.Fail("The expected exception was not thrown");
    }

    [TestMethod]
    public void CreateGroup_NameAtMinLength_ReturnsGroup()
    {
        Group group = new() { Name = "Duck" };

        Assert.AreEqual("Duck", group.Name);
    }

    [TestMethod]
    public void CreateGroup_NameAtMaxLength_ReturnsGroup()
    {
        Group group = new() { Name = "Quack, said the duckling!" };

        Assert.AreEqual("Quack, said the duckling!", group.Name);
    }

    [TestMethod]
    public void Renaming_WithTooShortName_ThrowsException()
    {
        Group group = new() { Name = "Expenses" };

        try
        {
            group.Name = "Bad";
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
        Group group = new() { Name = "Expenses" };

        try
        {
            group.Name = "This name is going to be way too long.";
        }
        catch (ArgumentException ex)
        {
            StringAssert.Contains(ex.Message, "Name is too long.");
            return;
        }

        Assert.Fail("The expected exception was not thrown");
    }
}