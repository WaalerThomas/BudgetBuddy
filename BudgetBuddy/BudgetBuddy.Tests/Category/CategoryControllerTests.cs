using AutoMapper;
using BudgetBuddy.Category.AutoMapper;
using BudgetBuddy.Category.Controller;
using BudgetBuddy.Category.Request;
using BudgetBuddy.Category.Resources;
using BudgetBuddy.Contracts.Interface.Category;
using BudgetBuddy.Contracts.Model.Category;
using BudgetBuddy.Core.Exceptions;
using BudgetBuddy.Tests.Common;
using NSubstitute;

namespace BudgetBuddy.Tests.Category;

[TestFixture]
public class CategoryControllerTests
{
    private CategoryController _controller;
    
    private ICategoryService _categoryService;
    private IMapper _mapper;
    
    [SetUp]
    public void SetUp()
    {
        _categoryService = Substitute.For<ICategoryService>();
        _mapper = new Mapper(new MapperConfiguration(
            cfg => cfg.AddProfile<CategoryProfile>()));
        
        _controller = new CategoryController(_mapper, _categoryService);
    }
    
    # region Update Category
    
    [Test]
    public void Update_WhenCategoryNotFound_ThrowsException()
    {
        // assert
        var exception = Assert.Throws<BuddyException>(
            () => _controller.Update(new UpdateCategoryRequest()));
        Assert.That(exception.Message, Is.EqualTo(CategoryResource.CategoryNotFound));
    }
    
    [Test]
    public void Update_WhenCategoryFound_UpdatesCategory()
    {
        // arrange
        var categoryModel = TestHelper.CreateCategory();
        _categoryService.Get(Arg.Any<int>()).Returns(categoryModel);
        
        // act
        var result = _controller.Update(new UpdateCategoryRequest());
        
        // assert
        _categoryService.Received().Update(Arg.Any<CategoryModel>());
    }
    
    # endregion
}