using BudgetBuddy.Models;

namespace BudgetBuddy.Controllers;

public class AccountController
{
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
}