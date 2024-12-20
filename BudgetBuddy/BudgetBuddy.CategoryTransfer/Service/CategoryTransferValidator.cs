﻿using BudgetBuddy.Contracts.Interface.Category;
using BudgetBuddy.Contracts.Model.CategoryTransfer;
using CategoryTransfer.Resources;
using FluentValidation;

namespace CategoryTransfer.Service;

public class CategoryTransferValidator : AbstractValidator<CategoryTransferModel>, ICategoryTransferValidator
{
    private readonly ICategoryService _categoryService;
    
    public CategoryTransferValidator(ICategoryService categoryService)
    {
        _categoryService = categoryService;
        
        RuleFor(x => x.FromCategoryId)
            .Null().WithMessage(CategoryTransferResource.FromCategoryIdNotNeeded)
            .When(x => x.FromAvailableToBudget);
        
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage(CategoryTransferResource.AmountGreaterThanZero);
    }
    
    public void ValidateTransfer(CategoryTransferModel model)
    {
        if (model.ToCategoryId == model.FromCategoryId)
        {
            throw new ValidationException(CategoryTransferResource.SameCategory);
        }
        
        var toCategory = _categoryService.Get(model.ToCategoryId);
        if (toCategory is null)
        {
            throw new ValidationException(CategoryTransferResource.ToCategoryNotFound);
        }

        if (toCategory.IsGroup)
        {
            throw new ValidationException(CategoryTransferResource.CanNotTransferToGroup);
        }

        if (!model.FromCategoryId.HasValue)
        {
            return;
        }
        
        var fromCategory = _categoryService.Get(model.FromCategoryId.Value);
        if (fromCategory == null)
        {
            throw new ValidationException(CategoryTransferResource.FromCategoryNotFound);
        }
        
        if (fromCategory.IsGroup)
        {
            throw new ValidationException(CategoryTransferResource.CanNotTransferFromGroup);
        }
    }
}