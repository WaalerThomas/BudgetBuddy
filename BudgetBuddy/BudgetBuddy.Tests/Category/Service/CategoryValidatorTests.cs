using BudgetBuddy.Category.Model;
using BudgetBuddy.Category.Repositories;
using BudgetBuddy.Category.Resources;
using BudgetBuddy.Category.Service;
using BudgetBuddy.Contracts.Enums;
using BudgetBuddy.Core.Exceptions;
using BudgetBuddy.Tests.Common;
using NSubstitute;

namespace BudgetBuddy.Tests.Category.Service;

[TestFixture]
public class CategoryValidatorTests
{
    private CategoryValidator _categoryValidator;
    
    private ICategoryRepository _categoryRepository;

    [SetUp]
    public void SetUp()
    {
        _categoryRepository = Substitute.For<ICategoryRepository>();
        
        _categoryValidator = new CategoryValidator(_categoryRepository);
    }

    [Test]
    public void ValidateOnGroup_WithMonthlyAmount_ShouldReturnErrorMessage()
    {
        // arrange
        var groupModel = TestHelper.CreateCategoryGroup();
        groupModel.MonthlyAmount = 100;
        
        // act
        var result = _categoryValidator.Validate(groupModel);
        
        // assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo(CategoryResource.GroupNeedNullMonthlyAmount));
        });
    }

    [Test]
    public void ValidateOnGroup_WithGoalAmount_ShouldReturnErrorMessage()
    {
        // arrange
        var groupModel = TestHelper.CreateCategoryGroup();
        groupModel.GoalAmount = 100;
        
        // act
        var result = _categoryValidator.Validate(groupModel);
        
        // assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo(CategoryResource.GroupNeedNullGoalAmount));
        });
    }
    
    [Test]
    public void ValidateOnGroup_WithGroupId_ShouldReturnErrorMessage()
    {
        // arrange
        var groupModel = TestHelper.CreateCategoryGroup();
        groupModel.GroupId = 1;
        
        // act
        var result = _categoryValidator.Validate(groupModel);
        
        // assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo(CategoryResource.AssigningGroupToGroup));
        });
    }
    
    [Test]
    public void ValidateOnCategory_WithNegativeMonthlyAmount_ShouldReturnErrorMessage()
    {
        // arrange
        var categoryModel = TestHelper.CreateCategory();
        categoryModel.MonthlyAmount = -100;
        
        // act
        var result = _categoryValidator.Validate(categoryModel);
        
        // assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo(CategoryResource.NeedPositiveMonthlyAmount));
        });
    }
    
    [Test]
    public void ValidateOnCategory_WithNoMonthlyAmount_ShouldReturnErrorMessage()
    {
        // arrange
        var categoryModel = TestHelper.CreateCategory();
        categoryModel.MonthlyAmount = null;
        
        // act
        var result = _categoryValidator.Validate(categoryModel);
        
        // assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo(CategoryResource.MonthlyAmountRequired));
        });
    }
    
    [Test]
    public void ValidateOnCategory_WithNegativeGoalAmount_ShouldReturnErrorMessage()
    {
        // arrange
        var categoryModel = TestHelper.CreateCategory();
        categoryModel.GoalAmount = -100;
        
        // act
        var result = _categoryValidator.Validate(categoryModel);
        
        // assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo(CategoryResource.NeedPositiveGoalAmount));
        });
    }
    
    [Test]
    public void ValidateOnCategory_WithNoGroupId_ShouldReturnErrorMessage()
    {
        // arrange
        var categoryModel = TestHelper.CreateCategory();
        categoryModel.GroupId = null;
        
        // act
        var result = _categoryValidator.Validate(categoryModel);
        
        // assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo(CategoryResource.GroupIdIsMissing));
        });
    }
    
    #region ValidateGroupAssignment
    
    [Test]
    public void ValidateGroupAssignment_WhenAGroup_ShouldSkipValidation()
    {
        // arrange
        var groupModel = TestHelper.CreateCategoryGroup();
        
        // act
        _categoryValidator.ValidateGroupAssignment(groupModel);
        
        // assert
        _categoryRepository.DidNotReceive().GetById(Arg.Any<int>());
    }

    [Test]
    public void ValidateGroupAssignment_WithNoGroupId_ShouldThrowException()
    {
        // arrange
        var categoryModel = TestHelper.CreateCategory(groupId: null);
        
        // assert
        var exception = Assert.Throws<BuddyException>(
            () => _categoryValidator.ValidateGroupAssignment(categoryModel));
        Assert.That(exception.Message, Is.EqualTo(CategoryResource.GroupIdIsMissing));
    }
    
    [Test]
    public void ValidateGroupAssignment_WithInvalidGroupId_ShouldThrowException()
    {
        // arrange
        var categoryModel = TestHelper.CreateCategory();
        
        // assert
        var exception = Assert.Throws<BuddyException>(
            () => _categoryValidator.ValidateGroupAssignment(categoryModel));
        Assert.That(exception.Message, Is.EqualTo(CategoryResource.CategoryGroupDoesNotExist));
    }

    [Test]
    public void ValidateGroupAssignment_WithACategoryAsGroup_ShouldThrowException()
    {
        // arrange
        var categoryModel = TestHelper.CreateCategory();
        _categoryRepository.GetById(Arg.Any<int>()).Returns(new CategoryDao { Type = CategoryType.Category });
        
        // assert
        var exception = Assert.Throws<BuddyException>(
            () => _categoryValidator.ValidateGroupAssignment(categoryModel));
        Assert.That(exception.Message, Is.EqualTo(CategoryResource.AssigningCategoryToCategory));
    }
    
    #endregion
}