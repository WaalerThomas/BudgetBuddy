using BudgetBuddy.Category.ViewModel;
using BudgetBuddy.Contracts.Model.Category;

namespace BudgetBuddy.Category.Service;

public interface ICategoryService
{
    CategoryModel? Get(int id);
    CategoryModel Create(CategoryModel categoryModel);
    IEnumerable<CategoryModel> GetCategories();
    IEnumerable<CategoryModel> GetGroups();
    CategoryModel Update(CategoryModel categoryModel);

    IEnumerable<GroupCategoryVm> GetGrouped();
}