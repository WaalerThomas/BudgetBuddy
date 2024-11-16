using AutoMapper;
using BudgetBuddy.Category.Repositories;
using BudgetBuddy.Contracts.Enums;
using BudgetBuddy.Contracts.Model.Category;
using BudgetBuddy.Core.Operation;

namespace BudgetBuddy.Category.Operations;

public class GetGroupedCategoriesOperation : Operation<object, IEnumerable<GroupCategoryModel>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetGroupedCategoriesOperation(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    protected override IEnumerable<GroupCategoryModel> OnOperate(object request)
    {
        var groups = _categoryRepository.GetByType(CategoryType.Group);
        
        // TODO: What type of edge-cases can there be here?
        
        var groupedCategories = new List<GroupCategoryModel>();
        foreach (var group in groups)
        {
            var categories = _categoryRepository.GetGroupsCategories(group.Id);
            
            groupedCategories.Add(new GroupCategoryModel
            {
                Group = _mapper.Map<GroupModel>(group),
                Categories = _mapper.Map<IEnumerable<CategoryModel>>(categories)
            });
        }

        return groupedCategories;
    }
}