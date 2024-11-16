using AutoMapper;
using BudgetBuddy.Contracts.Model.CategoryTransfer;
using BudgetBuddy.Core.Extensions;
using CategoryTransfer.AutoMapper;
using CategoryTransfer.Model;
using CategoryTransfer.Operations;
using CategoryTransfer.Repositories;
using CategoryTransfer.Service;
using FluentValidation.Results;
using NSubstitute;

namespace BudgetBuddy.Tests.CategoryTransfer.Operation;

[TestFixture]
public class CreateCategoryTransferOperationTests
{
    private CreateCategoryTransferOperation _operation;
    
    private ICategoryTransferRepository _categoryTransferRepository;
    private ICategoryTransferValidator _categoryTransferValidator;
    private IMapper _mapper;
    
    [SetUp]
    public void SetUp()
    {
        _categoryTransferRepository = Substitute.For<ICategoryTransferRepository>();
        _categoryTransferValidator = Substitute.For<ICategoryTransferValidator>();
        _mapper = new Mapper(new MapperConfiguration(
            cfg => cfg.AddProfile<CategoryTransferProfile>()));
        
        _operation = new CreateCategoryTransferOperation(
            _categoryTransferRepository,
            _categoryTransferValidator,
            _mapper);
    }
    
    [Test]
    public void WhenCreatingCategoryTransfer_ShouldCallRepository()
    {
        // arrange
        var categoryTransferModel = new CategoryTransferModel();
        _categoryTransferValidator.Validate(Arg.Any<CategoryTransferModel>()).Returns(new ValidationResult());
        _categoryTransferRepository.Create(Arg.Any<CategoryTransferDao>()).Returns(new CategoryTransferDao());
        
        // act
        _operation.Operate(categoryTransferModel);
        
        // assert
        _categoryTransferRepository.Received().Create(Arg.Any<CategoryTransferDao>());
    }
    
    [Test]
    public void WhenCreatingCategoryTransfer_ShouldCallValidator()
    {
        // arrange
        var categoryTransferModel = new CategoryTransferModel();
        _categoryTransferValidator.Validate(Arg.Any<CategoryTransferModel>()).Returns(new ValidationResult());
        _categoryTransferRepository.Create(Arg.Any<CategoryTransferDao>()).Returns(new CategoryTransferDao());
        
        // act
        _operation.Operate(categoryTransferModel);
        
        // assert
        _categoryTransferValidator.Received().Validate(Arg.Is(categoryTransferModel));
        _categoryTransferValidator.Received().ValidateTransfer(Arg.Is(categoryTransferModel));
    }
}