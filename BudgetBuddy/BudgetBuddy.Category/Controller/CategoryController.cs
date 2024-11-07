﻿using AutoMapper;
using BudgetBuddy.Category.Request;
using BudgetBuddy.Category.Service;
using BudgetBuddy.Category.ViewModel;
using BudgetBuddy.Contracts.Model.Category;
using BudgetBuddy.Contracts.Model.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BudgetBuddy.Category.Controller;

[Authorize]
[ApiController]
[Route("api/categories")]
public class CategoryController
{
    private readonly IMapper _mapper;
    private readonly ICategoryService _categoryService;

    public CategoryController(IMapper mapper, ICategoryService categoryService)
    {
        _mapper = mapper;
        _categoryService = categoryService;
    }

    [HttpPost]
    [EndpointSummary("Create a new category")]
    [EndpointDescription("Create a new category for the user")]
    public BuddyResponse<CategoryVm> Create([FromBody] CreateCategoryRequest createCategoryRequest)
    {
        var categoryModel = _mapper.Map<CreateCategoryRequest, CategoryModel>(createCategoryRequest);
        categoryModel = _categoryService.Create(categoryModel);
        
        return new BuddyResponse<CategoryVm>(_mapper.Map<CategoryModel, CategoryVm>(categoryModel));
    }
}