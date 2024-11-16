using BudgetBuddy.Contracts.Model.Category;
using FluentValidation;

namespace BudgetBuddy.Category.Service;

public interface ICategoryValidator : IValidator<CategoryModel>
{
    public void ValidateGroupAssignment(CategoryModel categoryModel);
}