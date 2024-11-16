using AutoMapper;
using BudgetBuddy.Category.Model;
using BudgetBuddy.Category.Operations;
using BudgetBuddy.Category.Repositories;
using BudgetBuddy.Category.Service;
using BudgetBuddy.Contracts.Model.Category;
using BudgetBuddy.Tests.Common;
using NSubstitute;

namespace BudgetBuddy.Tests.Category.Operation;

[TestFixture]
public class UpdateCategoryOperationTests
{
    private UpdateCategoryOperation _operation;
    
    private IMapper _mapper;
    private ICategoryValidator _categoryValidator;
    private ICategoryRepository _categoryRepository;
    
    [SetUp]
    public void SetUp()
    {
        _mapper = Substitute.For<IMapper>();
        _categoryValidator = Substitute.For<ICategoryValidator>();
        _categoryRepository = Substitute.For<ICategoryRepository>();
        
        _operation = new UpdateCategoryOperation(_categoryRepository, _categoryValidator, _mapper);
    }

    [Test]
    public void UpdateCategory_ShouldValidateGroupAssignment()
    {
        // act
        _operation.Operate(TestHelper.CreateCategory());
        
        // assert
        _categoryValidator.Received().ValidateGroupAssignment(Arg.Any<CategoryModel>());
    }
    
    [Test]
    public void UpdateCategory_ShouldCallUpdateOnRepository()
    {
        // act
        _operation.Operate(TestHelper.CreateCategory());
        
        // assert
        _categoryRepository.Received().Update(Arg.Any<CategoryDao>());
    }
    
    [Test]
    public void UpdateCategory_WithValidCategory_ShouldReturnCategoryModel()
    {
        // arrange
        var categoryModel = TestHelper.CreateCategory();
        var categoryDao = _mapper.Map<CategoryDao>(categoryModel);
        _categoryRepository.Update(Arg.Any<CategoryDao>()).Returns(categoryDao);
        _mapper.Map<CategoryModel>(Arg.Any<CategoryDao>()).Returns(categoryModel);
        
        // act
        var result = _operation.Operate(categoryModel);
        
        // assert
        Assert.That(result, Is.EqualTo(categoryModel));
    }
}