using AutoMapper;
using BudgetBuddy.Category.Repositories;
using BudgetBuddy.Contracts.Model.Category;
using BudgetBuddy.Core.Operation;

namespace BudgetBuddy.Category.Operations;

public class GetCategoryByIdOperation : Operation<int, CategoryModel?>
{
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryByIdOperation(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    protected override CategoryModel? OnOperate(int id)
    {
        var categoryDao = _categoryRepository.GetById(id);
        var categoryModel = _mapper.Map<CategoryModel?>(categoryDao);
        return categoryModel;
    }
}