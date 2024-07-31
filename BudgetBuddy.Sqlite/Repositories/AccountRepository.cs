using BudgetBuddy.Controllers;
using BudgetBuddy.Models;
using BudgetBuddy.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BudgetBuddy.Sqlite.Repositories;

/*
    - Add a DateTime on Account for when the balance was last updated.
    - When querying for accounts, check if there are any new transactions for account, then update the values.

    Problems:
    1. What if there is a pending entry? How will the calculation know when pending transactions have been updated?
*/

public class AccountRespository : Repository<Account>, IAccountRepository
{
    public DatabaseContext DatabaseContext => (DatabaseContext)Context;

    public AccountRespository(DatabaseContext context)
        : base(context)
    {
    }

    public override Account? Get(int id)
    {
        // TODO: Look into if there is a better approach to updating the balances.

        // NOTE: I might want to not always do a bunch of queries to the database for everytime I just want to get accounts. Maybe move stuff to AccountController as a method that client
        //       can call whenever they want the balances to recalculate (They can know how old )
        /*
        Account? account = DatabaseContext.Find<Account>(id);
        if (account is not null)
        {
            // Recalculate the pending- and settled balance
            var pendingBalance = DatabaseContext.Transactions
                .Where(t => t.Account!.Id == id
                    && t.TransactionStatus.Id == TransactionStatusEnum.Pending
                    && ((account.OldestPendingDate == null) || t.EntryDate >= account.OldestPendingDate))
                .OrderBy(t => t.EntryDate)
                .Select(t => new { t.EntryDate, t.Amount })
                .ToList();

            decimal pendingDiff = pendingBalance.Sum(t => t.Amount) - account.PendingBalance;

            DateTime? settledDateTime = account.CalculatedSettledDate;
            if (pendingDiff != 0)
            {
                // Pending entry has turned to settled. Need to recalculate from there
                settledDateTime = account.OldestPendingDate;
            }

            var settledBalance = DatabaseContext.Transactions
                .Where(t => t.Account!.Id == id
                    && t.TransactionStatus.Id == TransactionStatusEnum.Settled
                    && ((account.OldestPendingDate == null) || t.EntryDate >= settledDateTime))
                .Select(t => t.Amount).ToList();
        
            decimal settledDiff = settledBalance.Sum() - account.SettledBalance;

            if (settledDiff != 0 || pendingDiff != 0)
            {
                account.SettledBalance += settledDiff;
                account.PendingBalance += pendingDiff;
                account.CalculatedSettledDate = DateTime.Now;
                account.OldestPendingDate = pendingBalance.FirstOrDefault()?.EntryDate;

                // Update the Account in database since we don't want to give that job to the caller.
                // Also cannot use the callers uow, since we don't know if they have done changes that they
                // don't want to be sendt to the database yet.
                var uow = new UnitOfWork(new DatabaseContext());
                Account saveAccount = uow.Accounts.Find(a => a.Id == account.Id).Single();
                AccountController.CopyFromTo(account, saveAccount);
                uow.Complete();
            }
        }
        return account;
        */

        return base.Get(id);
    }

    public override IEnumerable<Account> GetAll()
    {
        // NOTE: I've implemented so that it only grabs entries from a certain datetime to present datetime because amount is stored as
        //       TEXT, which Sqlite cannot sum, so i am limiting the entries returned that gets summed in code.

        // TODO: Look into if we actually need CalculatedSettledDate

        var uow = new UnitOfWork(new DatabaseContext());

        // Check if the balances need to be recalculated.
        foreach (var account in DatabaseContext.Accounts)
        {
            if (account is null)    // Shouldnt really be anyone that is null
                continue;
            
            var pendingBalance = DatabaseContext.Transactions
                .Where(t => t.Account!.Id == account.Id
                    && t.TransactionStatus.Id == TransactionStatusEnum.Pending
                    && ( (account.OldestPendingDate == null) || t.EntryDate >= account.OldestPendingDate ))
                .OrderBy(t => t.EntryDate)
                .Select(t => new { t.EntryDate, t.Amount })
                .ToList();
            
            decimal pendingDiff = pendingBalance.Sum(t => t.Amount) - account.PendingBalance;

            // If pending balance has changed, set the datetime window to start from OldestPendingDate to check if the pending entry has
            // been changed to settled, or just removed.
            DateTime? settledDateTime = pendingDiff != 0 ? account.OldestPendingDate : account.CalculatedSettledDate;

            var settledBalance = DatabaseContext.Transactions
                .Where(t => t.Account!.Id == account.Id
                    && t.TransactionStatus.Id == TransactionStatusEnum.Settled
                    && ((account.OldestPendingDate == null) || t.EntryDate >= settledDateTime))
                .Select(t => t.Amount).ToList();
        
            decimal settledDiff = settledBalance.Sum() - account.SettledBalance;

            if (settledDiff != 0 || pendingDiff != 0)
            {
                account.SettledBalance += settledDiff;
                account.PendingBalance += pendingDiff;
                account.CalculatedSettledDate = DateTime.Now;
                account.OldestPendingDate = pendingBalance.FirstOrDefault()?.EntryDate;

                // Update the Account in database since we don't want to give that job to the caller.
                // Also cannot use the callers uow, since we don't know if they have done changes that they
                // don't want to be sendt to the database yet.
                Account saveAccount = uow.Accounts.Find(a => a.Id == account.Id).Single();
                AccountController.CopyFromTo(account, saveAccount);
                uow.Complete();
            }
        }

        return base.GetAll();
    }
}