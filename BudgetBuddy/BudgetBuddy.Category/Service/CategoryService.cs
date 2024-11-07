﻿using BudgetBuddy.Category.Operations;
using BudgetBuddy.Category.ViewModel;
using BudgetBuddy.Contracts.Model.Category;
using BudgetBuddy.Core.Operation;
using BudgetBuddy.Core.Service;

namespace BudgetBuddy.Category.Service;

public class CategoryService : ServiceBase, ICategoryService
{
    public CategoryService(IOperationFactory operationFactory) : base(operationFactory)
    {
    }
    
    public CategoryModel Create(CategoryModel categoryModel)
    {
        var operation = CreateOperation<CreateCategoryOperation>();
        return operation.Operate(categoryModel);
    }

    public IEnumerable<CategoryModel> GetCategories()
    {
        var operation = CreateOperation<GetCategoriesOperation>();
        return operation.Operate();
    }

    public IEnumerable<CategoryModel> GetGroups()
    {
        var operation = CreateOperation<GetGroupsOperation>();
        return operation.Operate();
    }

    public IEnumerable<GroupCategoryVm> GetGrouped()
    {
        var operation = CreateOperation<GetGroupedCategoriesOperation>();
        return operation.Operate();
    }
}