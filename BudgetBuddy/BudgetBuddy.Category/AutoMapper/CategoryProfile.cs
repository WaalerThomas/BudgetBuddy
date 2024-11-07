﻿using AutoMapper;
using BudgetBuddy.Category.Request;
using BudgetBuddy.Category.ViewModel;
using BudgetBuddy.Contracts.Model.Category;

namespace BudgetBuddy.Category.AutoMapper;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CreateCategoryRequest, CategoryModel>();

        CreateMap<CategoryModel, CategoryVm>();
    }
}