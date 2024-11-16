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

    public override IEnumerable<Account> GetAll()
    {
        // NOTE: I've implemented so that it only grabs entries from a certain datetime to present datetime because amount is stored as
        //       TEXT, which Sqlite cannot sum, so i am limiting the entries returned that gets summed in code.

        // TODO: Problem, I am trying to optimize stuff too soon, which is making systems too complicated for no reason. Just implement easy, and then profile it to optimize later.

        var uow = new UnitOfWork(new DatabaseContext());

        // Check if the balances need to be recalculated.
        foreach (var account in DatabaseContext.Accounts)
        {
            if (account is null)    // Shouldnt really be anyone that is null
                continue;
            
            var pendingBalance = DatabaseContext.Transactions
                .Where(t => t.Account!.Id == account.Id
                    && t.TransactionStatus.Id == TransactionStatusEnum.Pending)
                .Select(t => t.Amount)
                .ToList()
                .Sum();
            var settledBalance = DatabaseContext.Transactions
                .Where(t => t.Account!.Id == account.Id
                    && t.TransactionStatus.Id == TransactionStatusEnum.Settled)
                .Select(t => t.Amount)
                .ToList()
                .Sum();
            
            // If the calculated balance is different than the saved balance, update the saved balance
            if (account.PendingBalance != pendingBalance || account.SettledBalance != settledBalance)
            {
                account.SettledBalance = settledBalance;
                account.PendingBalance = pendingBalance;

                // Update the Account in database since we don't want to give that job to the caller.
                // Also cannot use the callers uow, since we don't know if they have done changes that they
                // don't want to be sendt to the database yet.
                Account saveAccount = uow.Accounts.Find(a => a.Id == account.Id).Single();
                AccountController.CopyFromTo(account, saveAccount);
                uow.Complete();
            }
            /*
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
            */
        }

        return base.GetAll();
    }
}