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
public class CreateCategoryOperationTests
{
    private CreateCategoryOperation _operation;

    private ICategoryValidator _categoryValidator;
    private ICategoryRepository _categoryRepository;
    private IMapper _mapper;
    
    [SetUp]
    public void Setup()
    {
        _categoryValidator = Substitute.For<ICategoryValidator>();
        _categoryRepository = Substitute.For<ICategoryRepository>();
        _mapper = Substitute.For<IMapper>();
        
        _operation = new CreateCategoryOperation(_categoryValidator, _categoryRepository, _mapper);
    }

    [Test]
    public void CreateCategory_ShouldValidateModel()
    {
        // act
        _operation.Operate(TestHelper.CreateCategory());
        
        // assert
        _categoryValidator.Received().ValidateGroupAssignment(Arg.Any<CategoryModel>());
    }
    
    [Test]
    public void CreateCategory_ShouldCallCreateOnRepository()
    {
        // act
        _operation.Operate(TestHelper.CreateCategory());
        
        // assert
        _categoryRepository.Received().Create(Arg.Any<CategoryDao>());
    }
}