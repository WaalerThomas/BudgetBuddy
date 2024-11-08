using AutoMapper;
using BudgetBuddy.Category.Model;
using BudgetBuddy.Category.Repositories;
using BudgetBuddy.Category.Service;
using BudgetBuddy.Contracts.Model.Category;
using BudgetBuddy.Core.Operation;
using FluentValidation;

namespace BudgetBuddy.Category.Operations;

public class CreateCategoryOperation : Operation<CategoryModel, CategoryModel>
{
    private readonly IMapper _mapper;
    private readonly ICategoryValidator _categoryValidator;
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryOperation(
        ICategoryValidator categoryValidator,
        ICategoryRepository categoryRepository,
        IMapper mapper)
    {
        _categoryValidator = categoryValidator;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    protected override CategoryModel OnOperate(CategoryModel categoryModel)
    {
        _categoryValidator.ValidateAndThrow(categoryModel);
        _categoryValidator.ValidateGroupAssignment(categoryModel);
        
        var categoryDao = _mapper.Map<CategoryDao>(categoryModel);
        categoryDao = _categoryRepository.Create(categoryDao);
        
        categoryModel = _mapper.Map<CategoryModel>(categoryDao);
        return categoryModel;
    }
}