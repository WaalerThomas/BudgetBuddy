using BudgetBuddy.Contracts.Model.Category;

namespace BudgetBuddy.Category.Service;

public interface ICategoryService
{
    CategoryModel Create(CategoryModel categoryModel);
    IEnumerable<CategoryModel> GetCategories();
}