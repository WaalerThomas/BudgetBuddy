using AutoMapper;
using BudgetBuddy.Category.Repositories;
using BudgetBuddy.Category.ViewModel;
using BudgetBuddy.Contracts.Model.Category;
using BudgetBuddy.Core.Operation;

namespace BudgetBuddy.Category.Operations;

public class GetGroupedCategoriesOperation : Operation<object, IEnumerable<GroupCategoryVm>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetGroupedCategoriesOperation(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    protected override IEnumerable<GroupCategoryVm> OnOperate(object request)
    {
        var groups = _categoryRepository.GetByType(true);
        
        // TODO: What type of edge-cases can there be here?
        
        var groupedCategories = new List<GroupCategoryVm>();
        foreach (var group in groups)
        {
            var categories = _categoryRepository.GetGroupsCategories(group.Id);
            
            groupedCategories.Add(new GroupCategoryVm
            {
                Group = _mapper.Map<CategoryModel, GroupVm>(group),
                Categories = _mapper.Map<IEnumerable<CategoryModel>, IEnumerable<CategoryVm>>(categories)
            });
        }

        return groupedCategories;
    }
}