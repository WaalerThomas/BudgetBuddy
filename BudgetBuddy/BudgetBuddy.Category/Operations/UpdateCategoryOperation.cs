using AutoMapper;
using BudgetBuddy.Category.Model;
using BudgetBuddy.Category.Repositories;
using BudgetBuddy.Category.Service;
using BudgetBuddy.Contracts.Model.Category;
using BudgetBuddy.Core.Operation;
using FluentValidation;

namespace BudgetBuddy.Category.Operations;

public class UpdateCategoryOperation : Operation<CategoryModel, CategoryModel>
{
    private readonly IMapper _mapper;
    private readonly ICategoryValidator _categoryValidator;
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryOperation(
        ICategoryRepository categoryRepository,
        ICategoryValidator categoryValidator,
        IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _categoryValidator = categoryValidator;
        _mapper = mapper;
    }

    protected override CategoryModel OnOperate(CategoryModel categoryModel)
    {
        _categoryValidator.ValidateAndThrow(categoryModel);
        _categoryValidator.ValidateGroupAssignment(categoryModel);

        var categoryDao = _mapper.Map<CategoryDao>(categoryModel);
        categoryDao = _categoryRepository.Update(categoryDao);
        
        categoryModel = _mapper.Map<CategoryModel>(categoryDao);
        return categoryModel;
    }
}