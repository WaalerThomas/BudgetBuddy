﻿using BudgetBuddy.Category.AutoMapper;
using BudgetBuddy.Category.Operations;
using BudgetBuddy.Category.Repositories;
using BudgetBuddy.Category.Service;
using BudgetBuddy.Contracts.Interface.Category;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetBuddy.Category;

public static class DependencyInjection
{
    public static IServiceCollection AddCategory(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(CategoryProfile));

        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICategoryValidator, CategoryValidator>();

        services.AddScoped<CreateCategoryOperation>();
        services.AddScoped<GetCategoryByIdOperation>();
        services.AddScoped<GetCategoriesOperation>();
        services.AddScoped<GetGroupsOperation>();
        services.AddScoped<GetGroupedCategoriesOperation>();
        services.AddScoped<UpdateCategoryOperation>();
        
        return services;
    }
}