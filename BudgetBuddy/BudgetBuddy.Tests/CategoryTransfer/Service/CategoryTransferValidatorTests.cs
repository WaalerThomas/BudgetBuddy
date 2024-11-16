using BudgetBuddy.Contracts.Interface.Category;
using BudgetBuddy.Contracts.Model.CategoryTransfer;
using BudgetBuddy.Tests.Common;
using CategoryTransfer.Resources;
using CategoryTransfer.Service;
using FluentValidation;
using NSubstitute;

namespace BudgetBuddy.Tests.CategoryTransfer.Service;

[TestFixture]
public class CategoryTransferValidatorTests
{
    private CategoryTransferValidator _validator;
    private ICategoryService _categoryService;
    
    [SetUp]
    public void SetUp()
    {
        _categoryService = Substitute.For<ICategoryService>();
        _validator = new CategoryTransferValidator(_categoryService);
    }

    [Test]
    public void WhenTransferringFromAvailableToBudgetAndFromCategoryIdIsSet_ShouldThrowException()
    {
        // arrange
        var categoryTransferModel = new CategoryTransferModel
        {
            FromAvailableToBudget = true,
            FromCategoryId = 1
        };
        
        // act
        var result = _validator.Validate(categoryTransferModel);
        
        Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo(CategoryTransferResource.FromCategoryIdNotNeeded));
    }
    
    [Test]
    public void WhenAmountIsLessThanZero_ShouldThrowException()
    {
        // arrange
        var categoryTransferModel = new CategoryTransferModel
        {
            Amount = -1
        };
        
        // act
        var result = _validator.Validate(categoryTransferModel);
        
        Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo("Amount must be greater than 0"));
    }

    #region Validate Transfer

    [Test]
    public void ValidateTransfer_WhenToCategoryIdIsSameAsFromCategoryId_ShouldThrowException()
    {
        // arrange
        var categoryTransferModel = new CategoryTransferModel
        {
            ToCategoryId = 1,
            FromCategoryId = 1
        };
        
        // assert
        var exception = Assert.Throws<ValidationException>(() => _validator.ValidateTransfer(categoryTransferModel));
        Assert.That(exception.Message, Is.EqualTo(CategoryTransferResource.SameCategory));
    }

    [Test]
    public void ValidateTransfer_WhenToCategoryIsNotFound_ShouldThrowException()
    {
        // arrange
        var categoryTransferModel = new CategoryTransferModel { ToCategoryId = 1 };

        // assert
        var exception = Assert.Throws<ValidationException>(() => _validator.ValidateTransfer(categoryTransferModel));
        Assert.That(exception.Message, Is.EqualTo(CategoryTransferResource.ToCategoryNotFound));
    }
    
    [Test]
    public void ValidateTransfer_WhenToCategoryIsGroup_ShouldThrowException()
    {
        // arrange
        var categoryTransferModel = new CategoryTransferModel { ToCategoryId = 1 };
        _categoryService.Get(Arg.Any<int>()).Returns(TestHelper.CreateCategoryGroup());

        // assert
        var exception = Assert.Throws<ValidationException>(() => _validator.ValidateTransfer(categoryTransferModel));
        Assert.That(exception.Message, Is.EqualTo(CategoryTransferResource.CanNotTransferToGroup));
    }
    
    [Test]
    public void ValidateTransfer_WhenFromCategoryIdIsNotSet_ShouldNotThrowException()
    {
        // arrange
        var categoryTransferModel = new CategoryTransferModel { ToCategoryId = 1 };
        _categoryService.Get(Arg.Any<int>()).Returns(TestHelper.CreateCategory());

        // act
        _validator.ValidateTransfer(categoryTransferModel);
        
        // assert
        Assert.Pass();
    }

    [Test]
    public void ValidateTransfer_WhenFromCategoryIsNotFound_ShouldThrowException()
    {
        // arrange
        var categoryTransferModel = new CategoryTransferModel { ToCategoryId = 1, FromCategoryId = 2 };
        _categoryService.Get(categoryTransferModel.ToCategoryId).Returns(TestHelper.CreateCategory());

        // assert
        var exception = Assert.Throws<ValidationException>(() => _validator.ValidateTransfer(categoryTransferModel));
        Assert.That(exception.Message, Is.EqualTo(CategoryTransferResource.FromCategoryNotFound));
    }
    
    [Test]
    public void ValidateTransfer_WhenFromCategoryIsGroup_ShouldThrowException()
    {
        // arrange
        var categoryTransferModel = new CategoryTransferModel { ToCategoryId = 1, FromCategoryId = 2 };
        _categoryService.Get(categoryTransferModel.ToCategoryId).Returns(TestHelper.CreateCategory());
        _categoryService.Get(categoryTransferModel.FromCategoryId.Value).Returns(TestHelper.CreateCategoryGroup());

        // assert
        var exception = Assert.Throws<ValidationException>(() => _validator.ValidateTransfer(categoryTransferModel));
        Assert.That(exception.Message, Is.EqualTo(CategoryTransferResource.CanNotTransferFromGroup));
    }

    #endregion
}