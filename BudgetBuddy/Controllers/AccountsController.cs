using BudgetBuddy.Models;

namespace BudgetBuddy.Controllers;

public class AccountController
{
    public const int MIN_NAME_LENGTH = 4;       // TODO: Move these length values in a common spot. Used in multiple places
    public const int MAX_NAME_LENGTH = 127;

    public static void CopyFromTo(Account fromAccount, Account toAccount)
    {
        // NOTE: The problem with making a method like this, is when we change or add fields to Account class. Then not everything will be copied over.
        // TODO: Check that the fields don't pass by reference

        toAccount.Id = fromAccount.Id;
        toAccount.Name = fromAccount.Name;
        toAccount.SettledBalance = fromAccount.SettledBalance;
        toAccount.PendingBalance = fromAccount.PendingBalance;
        toAccount.CalculatedSettledDate = fromAccount.CalculatedSettledDate;
        toAccount.OldestPendingDate = fromAccount.OldestPendingDate;
    }

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