using AutoMapper;
using BudgetBuddy.Contracts.Interface.CategoryTransfer;
using BudgetBuddy.Contracts.Model.CategoryTransfer;
using BudgetBuddy.Contracts.Model.Common;
using CategoryTransfer.Request;
using CategoryTransfer.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CategoryTransfer.Controller;

[Authorize]
[ApiController]
[Route("api/category-transfers")]
public class CategoryTransferController
{
    private readonly ICategoryTransferService _categoryTransferService;
    private readonly IMapper _mapper;

    public CategoryTransferController(ICategoryTransferService categoryTransferService, IMapper mapper)
    {
        _categoryTransferService = categoryTransferService;
        _mapper = mapper;
    }

    [HttpPost]
    [EndpointSummary("Create a new category transfer")]
    public BuddyResponse<CategoryTransferVm> Create(CreateCategoryTransferRequest createCategoryTransferRequest)
    {
        var categoryTransferModel = _mapper.Map<CategoryTransferModel>(createCategoryTransferRequest);
        categoryTransferModel = _categoryTransferService.Create(categoryTransferModel);
        return new BuddyResponse<CategoryTransferVm>(_mapper.Map<CategoryTransferVm>(categoryTransferModel));
    }
    
    [HttpGet("available-to-budget")]
    [EndpointSummary("Get the amount available to budget")]
    public BuddyResponse<decimal> GetAvailableToBudget()
    {
        return new BuddyResponse<decimal>(_categoryTransferService.GetAvailableToBudget());
    }
}