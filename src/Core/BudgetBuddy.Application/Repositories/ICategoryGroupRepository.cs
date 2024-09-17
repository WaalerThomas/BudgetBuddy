using BudgetBuddy.Domain.Entities.CategoryGroups;

namespace BudgetBuddy.Application.Repositories;

public interface ICategoryGroupRepository
{
    CategoryGroup? GetById(int id);
    IEnumerable<CategoryGroup> GetAll();
    void Add(CategoryGroup categoryGroup);
    void Update(CategoryGroup categoryGroup);
    void Delete(int id);
}