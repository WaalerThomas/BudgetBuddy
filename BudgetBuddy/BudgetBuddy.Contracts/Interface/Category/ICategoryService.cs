using BudgetBuddy.Contracts.Model.Category;

namespace BudgetBuddy.Contracts.Interface.Category;

public interface ICategoryService
{
    CategoryModel? Get(int id);
    CategoryModel Create(CategoryModel categoryModel);
    IEnumerable<CategoryModel> GetCategories();
    IEnumerable<CategoryModel> GetGroups();
    CategoryModel Update(CategoryModel categoryModel);

    IEnumerable<GroupCategoryModel> GetGrouped();
}