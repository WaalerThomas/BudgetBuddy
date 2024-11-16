using AutoMapper;
using BudgetBuddy.Category.Repositories;
using BudgetBuddy.Contracts.Enums;
using BudgetBuddy.Contracts.Model.Category;
using BudgetBuddy.Core.Operation;

namespace BudgetBuddy.Category.Operations;

public class GetGroupsOperation : Operation<object, IEnumerable<CategoryModel>>
{
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;

    public GetGroupsOperation(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    protected override IEnumerable<CategoryModel> OnOperate(object request)
    {
        var groups = _categoryRepository.GetByType(CategoryType.Group);
        var categoryModels = _mapper.Map<IEnumerable<CategoryModel>>(groups);
        return categoryModels;
    }
}