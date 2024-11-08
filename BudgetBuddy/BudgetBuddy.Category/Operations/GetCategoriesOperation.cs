using AutoMapper;
using BudgetBuddy.Category.Repositories;
using BudgetBuddy.Contracts.Enums;
using BudgetBuddy.Contracts.Model.Category;
using BudgetBuddy.Core.Operation;

namespace BudgetBuddy.Category.Operations;

public class GetCategoriesOperation : Operation<CategoryType, IEnumerable<CategoryModel>>
{
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoriesOperation(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    protected override IEnumerable<CategoryModel> OnOperate(CategoryType categoryType)
    {
        var categories = _categoryRepository.GetByType(categoryType);
        var categoryModels = _mapper.Map<IEnumerable<CategoryModel>>(categories);
        return categoryModels;
    }
}