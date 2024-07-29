using BudgetBuddy.Models;

namespace BudgetBuddy.Controllers;

public class AccountController
{
    public const int MIN_NAME_LENGTH = 4;
    public const int MAX_NAME_LENGTH = 127;

    public static Account CreateAccount(string name)
    {
        // TODO: Implement more rules for the naming of accounts. For example, not being allowed to start on a number.

        if (name.Length < MIN_NAME_LENGTH)
            throw new ArgumentException($"Name is too short. Minimum {MIN_NAME_LENGTH} characters long.", nameof(name));
        if (name.Length > MAX_NAME_LENGTH)
            throw new ArgumentException($"Name is too long. Maximum {MAX_NAME_LENGTH} characters long.", nameof(name));

        return new Account() { Name = name };
    }
}