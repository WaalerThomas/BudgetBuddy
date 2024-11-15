using AutoMapper;
using BudgetBuddy.Contracts.Model.CategoryTransfer;
using BudgetBuddy.Core.Operation;
using CategoryTransfer.Model;
using CategoryTransfer.Repositories;
using CategoryTransfer.Service;
using FluentValidation;

namespace CategoryTransfer.Operations;

public class CreateCategoryTransferOperation : Operation<CategoryTransferModel, CategoryTransferModel>
{
    private readonly ICategoryTransferRepository _categoryTransferRepository;
    private readonly ICategoryTransferValidator _categoryTransferValidator;
    private readonly IMapper _mapper;

    public CreateCategoryTransferOperation(
        ICategoryTransferRepository categoryTransferRepository,
        ICategoryTransferValidator categoryTransferValidator,
        IMapper mapper)
    {
        _categoryTransferRepository = categoryTransferRepository;
        _categoryTransferValidator = categoryTransferValidator;
        _mapper = mapper;
    }

    protected override CategoryTransferModel OnOperate(CategoryTransferModel categoryTransferModel)
    {
        _categoryTransferValidator.ValidateAndThrow(categoryTransferModel);
        _categoryTransferValidator.ValidateTransfer(categoryTransferModel);
        
        var categoryTransferDao = _mapper.Map<CategoryTransferDao>(categoryTransferModel);
        categoryTransferDao = _categoryTransferRepository.Create(categoryTransferDao);
        
        categoryTransferModel = _mapper.Map<CategoryTransferModel>(categoryTransferDao);
        return categoryTransferModel;
    }
}