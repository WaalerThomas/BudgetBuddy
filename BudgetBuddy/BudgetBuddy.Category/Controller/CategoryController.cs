using BudgetBuddy.Category.ViewModel;
using BudgetBuddy.Contracts.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace BudgetBuddy.Category.Controller;

[ApiController]
[Route("api/categories")]
public class CategoryController
{
    [HttpPost]
    public BuddyResponse<CategoryVm> Create(CategoryVm category)
    {
        throw new NotImplementedException();
    }
}