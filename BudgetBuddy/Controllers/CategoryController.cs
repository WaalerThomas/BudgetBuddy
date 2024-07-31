using BudgetBuddy.Models;

namespace BudgetBuddy.Controllers;

public class CategoryController
{
    public const int MIN_NAME_LENGTH = 4;       // TODO: Move these length values in a common spot. Used in multiple places
    public const int MAX_NAME_LENGTH = 25;

    public static Category CreateCategory(string name)
    {
        if (name.Length < MIN_NAME_LENGTH)
            throw new ArgumentException($"Name is too short. Minimum {MIN_NAME_LENGTH} characters long.", nameof(name));
        if (name.Length > MAX_NAME_LENGTH)
            throw new ArgumentException($"Name is too long. Maximum {MAX_NAME_LENGTH} characters long.", nameof(name));
        
        return new Category() { Name = name };
    }
}