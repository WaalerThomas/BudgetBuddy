using FluentValidation;
using FluentValidation.Results;

namespace BudgetBuddy.Core.Extensions;

public static class FluentValidationExtensions
{
    public static ValidationResult ValidateAndThrowException<T>(this IValidator<T> validator, T obj)
    {
        var result = validator.Validate(obj);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors.First().ErrorMessage);
        }
        return result;
    }
}