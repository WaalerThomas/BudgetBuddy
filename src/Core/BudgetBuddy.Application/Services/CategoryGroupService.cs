using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Application.Repositories;
using BudgetBuddy.Domain.Common;
using BudgetBuddy.Domain.Entities.CategoryGroups;
using BudgetBuddy.Domain.Services;

namespace BudgetBuddy.Application.Services;

public class CategoryGroupService : ICategoryGroupService
{
    private readonly IUnitOfWork _uow;
    private readonly ICategoryGroupRepository _categoryGroupRepository;

    public CategoryGroupService(
        IUnitOfWork unitOfWork,
        ICategoryGroupRepository categoryGroupRepository
    )
    {
        _uow = unitOfWork;
        _categoryGroupRepository = categoryGroupRepository;
    }

    public Result<int> CreateCategoryGroup(string name)
    {
        if (string.IsNullOrEmpty(name))
            return Result.Failure<int>(Error.NullValue);
        
        CategoryGroup group = new() { Name = name };
        _categoryGroupRepository.Add(group);

        // TODO: Do some checks to see if the adding completed? Want to catch if there is problems with the adding, for example if a CategoryGroup with the given name is already registered.
        _uow.Complete();

        return Result.Success(group.Id); 
    }
}