﻿using BudgetBuddy.Contracts.Model.Category;
using BudgetBuddy.Core.Repositories;

namespace BudgetBuddy.Category.Repositories;

public interface ICategoryRepository : IBuddyRepository<CategoryModel>
{
}