using BudgetBuddy.Controllers;
using BudgetBuddy.Models;

namespace BudgetBuddy.Test;

[TestClass]
public class CategoryTest
{
    [TestMethod]
    public void CreateCategory_WithValidName_ReturnsCategory()
    {
        Category category = CategoryController.CreateCategory("Gasoline");

        Assert.AreEqual("Gasoline", category.Name);
    }

    [TestMethod]
    public void CreateCategory_WithTooShortName_ThrowsException()
    {
        try
        {
            CategoryController.CreateCategory("Bad");
        }
        catch (ArgumentException ex)
        {
            StringAssert.Contains(ex.Message, "Name is too short.");
            return;
        }

        Assert.Fail("The expected exception was not thrown");
    }

    [TestMethod]
    public void CreateCategory_WithTooLongName_ThrowsException()
    {
        try
        {
            CategoryController.CreateCategory("This name is going to be way too long.");
        }
        catch (ArgumentException ex)
        {
            StringAssert.Contains(ex.Message, "Name is too long.");
            return;
        }

        Assert.Fail("The expected exception was not thrown");
    }

    [TestMethod]
    public void CreateCategory_NameAtMinLength_ReturnsCategory()
    {
        Category category = CategoryController.CreateCategory("Duck");

        Assert.AreEqual("Duck", category.Name);
    }

    [TestMethod]
    public void CreateCategory_NameAtMaxLength_ReturnsCategory()
    {
        Category category = CategoryController.CreateCategory("Quack, said the duckling!");

        Assert.AreEqual("Quack, said the duckling!", category.Name);
    }
}