using AutoMapper;
using BudgetBuddy.Category.Request;
using BudgetBuddy.Category.Service;
using BudgetBuddy.Category.ViewModel;
using BudgetBuddy.Contracts.Model.Category;
using BudgetBuddy.Contracts.Model.Common;
using BudgetBuddy.Core.Exceptions;
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
    
    [HttpGet("getGrouped")]
    [EndpointSummary("Get all categories")]
    [EndpointDescription("Get all categories grouped by category-group")]
    public BuddyResponse<IEnumerable<GroupCategoryVm>> GetAllGrouped()
    {
        var groupedCategories = _categoryService.GetGrouped();
        
        return new BuddyResponse<IEnumerable<GroupCategoryVm>>(groupedCategories);
    }
    
    [HttpGet("groups")]
    [EndpointSummary("Get all category groups")]
    [EndpointDescription("Get all category groups that the user has access to")]
    public BuddyResponse<IEnumerable<GroupVm>> GetAllGroups()
    {
        var categoryModels = _categoryService.GetGroups();
        var groups = _mapper.Map<IEnumerable<CategoryModel>, IEnumerable<GroupVm>>(categoryModels);
        
        return new BuddyResponse<IEnumerable<GroupVm>>(groups);
    }
    
    [HttpGet]
    [EndpointSummary("Get all categories")]
    [EndpointDescription("Get all categories that the user has access to, excluding groups")]
    public BuddyResponse<IEnumerable<CategoryVm>> GetAll()
    {
        var categoryModels = _categoryService.GetCategories();
        var categories = _mapper.Map<IEnumerable<CategoryModel>, IEnumerable<CategoryVm>>(categoryModels);
        
        return new BuddyResponse<IEnumerable<CategoryVm>>(categories);
    }
    
    [HttpPatch]
    [EndpointSummary("Update a category")]
    [EndpointDescription("Update a category for the user")]
    public BuddyResponse<CategoryVm> Update([FromBody] UpdateCategoryRequest updateCategoryRequest)
    {
        var categoryModel = _categoryService.Get(updateCategoryRequest.Id);
        if (categoryModel is null)
        {
            throw new BuddyException("Category not found");
        }
        
        categoryModel.Name = updateCategoryRequest.Name;
        categoryModel.MonthlyAmount = updateCategoryRequest.MonthlyAmount;
        categoryModel.GoalAmount = updateCategoryRequest.GoalAmount;
        categoryModel.GroupId = updateCategoryRequest.GroupId;
        
        categoryModel = _categoryService.Update(categoryModel);
        
        return new BuddyResponse<CategoryVm>(_mapper.Map<CategoryModel, CategoryVm>(categoryModel));
    }
}