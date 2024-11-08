using BudgetBuddy.Category.Service;
using BudgetBuddy.Core.Operation;
using NSubstitute;

namespace BudgetBuddy.Tests.Category.Service;

[TestFixture]
public class CategoryServiceTests
{
    private CategoryService _categoryService;
    
    private IOperationFactory _operationFactory;
    
    [SetUp]
    public void Setup()
    {
        _operationFactory = Substitute.For<IOperationFactory>();
        
        _categoryService = new CategoryService(_operationFactory);
    }
}