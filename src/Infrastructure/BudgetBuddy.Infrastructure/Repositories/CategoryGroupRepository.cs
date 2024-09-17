using BudgetBuddy.Application.Repositories;
using BudgetBuddy.Domain.Entities.CategoryGroups;

namespace BudgetBuddy.Infrastructure.Repositories;

public class CategoryGroupRepository : ICategoryGroupRepository
{
    List<CategoryGroup> categoryGroups = [];

    public void Add(CategoryGroup categoryGroup)
    {
        // NOTE: Could this be seen as a strange side-effect?
        categoryGroups.Add(categoryGroup);
        categoryGroup.Id = categoryGroups.FindIndex(g => g.Name == categoryGroup.Name);
    }

    public void Delete(int id)
    {
        CategoryGroup categoryGroup = categoryGroups.Find(g => g.Id == id) ?? throw new ArgumentException("Id not found");
        categoryGroups.Remove(categoryGroup);
    }

    public IEnumerable<CategoryGroup> GetAll()
    {
        return categoryGroups;
    }

    public CategoryGroup? GetById(int id)
    {
        return categoryGroups.Find(g => g.Id == id);
    }

    public void Update(CategoryGroup categoryGroup)
    {
        int groupIndex = categoryGroups.FindIndex(g => g.Id == categoryGroup.Id);
        if (groupIndex == -1)
            throw new ArgumentException("Provided CategoryGroup id is not found");

        categoryGroups[groupIndex] = categoryGroup;
    }
}