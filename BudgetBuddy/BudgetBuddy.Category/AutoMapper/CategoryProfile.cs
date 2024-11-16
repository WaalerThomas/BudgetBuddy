using AutoMapper;
using BudgetBuddy.Category.Model;
using BudgetBuddy.Category.Request;
using BudgetBuddy.Category.ViewModel;
using BudgetBuddy.Contracts.Model.Category;

namespace BudgetBuddy.Category.AutoMapper;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CreateCategoryRequest, CategoryModel>();
        CreateMap<UpdateCategoryRequest, CategoryModel>();

        CreateMap<CategoryModel, CategoryVm>();
        CreateMap<CategoryModel, GroupVm>();
        
        CreateMap<CategoryModel, CategoryDao>()
            .ReverseMap();

        CreateMap<CategoryDao, CategoryVm>();
        CreateMap<CategoryDao, GroupModel>();
        
        CreateMap<GroupCategoryModel, GroupCategoryVm>();
        CreateMap<GroupModel, GroupVm>();
    }
}