using BudgetBuddy.Contracts.Model.CategoryTransfer;
using FluentValidation;

namespace CategoryTransfer.Service;

public interface ICategoryTransferValidator : IValidator<CategoryTransferModel>
{
}