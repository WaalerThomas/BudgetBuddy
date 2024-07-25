using BudgetBuddyCore.Models;

namespace BudgetBuddyCore.Interfaces;

public interface ICategoryRepository
{
    IEnumerable<Category> GetCategories();
    IEnumerable<Category> GetCategories(Group group);
    Category? GetCategory(int id);
    Category? GetCategoryByName(string name);
    bool Insert(Category category);
    bool Delete(int id);
    bool Update(Category category);
}