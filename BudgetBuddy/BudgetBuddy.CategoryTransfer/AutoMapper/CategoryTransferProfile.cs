using AutoMapper;
using BudgetBuddy.Contracts.Model.CategoryTransfer;
using CategoryTransfer.Model;
using CategoryTransfer.Request;
using CategoryTransfer.ViewModel;

namespace CategoryTransfer.AutoMapper;

public class CategoryTransferProfile : Profile
{
    public CategoryTransferProfile()
    {
        CreateMap<CategoryTransferModel, CategoryTransferDao>()
            .ReverseMap();

        CreateMap<CategoryTransferModel, CategoryTransferVm>()
            .ReverseMap();

        CreateMap<CreateCategoryTransferRequest, CategoryTransferModel>();
    }
}