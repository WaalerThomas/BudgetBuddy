using BudgetBuddy.Common.Service;
using BudgetBuddy.Contracts.Enums;
using BudgetBuddy.Contracts.Model.Account;
using BudgetBuddy.Transaction.Model;
using BudgetBuddy.Transaction.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BudgetBuddy.Data.Repositories;

public class TransactionRepository : Repository<TransactionDao>, ITransactionRepository
{
    public TransactionRepository(
        DatabaseContext context, 
        ICurrentUserService currentUser) : base(context, currentUser)
    {
    }

    public TransactionDao Create(TransactionDao model)
    {
        model.ClientId = _currentUser.ClientId;
        model.CreatedAt = DateTime.Now;
        
        var result = Context.Transactions.Add(model);
        
        Context.SaveChanges();
        
        return result.Entity;
    }

    public Task<TransactionDao> CreateAsync(TransactionDao model)
    {
        throw new NotImplementedException();
    }

    public TransactionDao Update(TransactionDao model)
    {
        model.UpdatedAt = DateTime.Now;
        
        var result = Context.Transactions.Update(model);

        Context.SaveChanges();
        
        return result.Entity;
    }

    public Task<TransactionDao> UpdateAsync(TransactionDao model)
    {
        throw new NotImplementedException();
    }

    public void Delete(TransactionDao model)
    {
        throw new NotImplementedException();
    }

    public TransactionDao? GetById(int id)
    {
        return Context.Transactions
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == id && x.ClientId == _currentUser.ClientId);
    }

    public IEnumerable<TransactionDao> GetByIds(IEnumerable<int> ids)
    {
        throw new NotImplementedException();
    }

    public decimal GetFlowSum()
    {
        return Context.Transactions
            .AsNoTracking()
            .Where(x => x.ClientId == _currentUser.ClientId && x.Type == TransactionType.BalanceAdjustment)
            .Select(x => x.Amount)
            .ToList()
            .Sum();
    }

    public AccountBalanceModel GetBalance(int accountId, bool onlyActualBalance)
    {
        if (onlyActualBalance)
        {
            return new AccountBalanceModel
            {
                Id = accountId,
                ActualBalance = GetActualBalance(accountId)
            };
        }
        
        // TODO: When showing settled and pending balance, we shouldn't need to calculate the actual balance
        var settledBalance = GetSettledBalance(accountId);
        var pendingBalance = GetPendingBalance(accountId);
        return new AccountBalanceModel
        {
            Id = accountId,
            PendingBalance = pendingBalance,
            SettledBalance = settledBalance
        };
    }

    public override IEnumerable<TransactionDao> GetAll()
    {
        return Context.Transactions
            .AsNoTracking()
            .Where(x => x.ClientId == _currentUser.ClientId)
            .ToList();
    }

    private decimal GetActualBalance(int accountId)
    {
        var mainBalanceSum = Context.Transactions
            .AsNoTracking()
            .Where(x =>
                x.ClientId == _currentUser.ClientId
                && (
                    (x.Type == TransactionType.Category && x.FromAccountId == accountId)
                    || (x.Type == TransactionType.BalanceAdjustment && x.ToAccountId == accountId)
                    || (x.Type == TransactionType.AccountTransfer && x.ToAccountId == accountId)
                ))
            .Select(x => x.Amount)
            .ToList()
            .Sum();
        
        var balanceAccountTransferFrom = Context.Transactions
            .AsNoTracking()
            .Where(x =>
                x.ClientId == _currentUser.ClientId
                && x.Type == TransactionType.AccountTransfer
                && x.FromAccountId == accountId)
            .Select(x => x.Amount)
            .ToList()
            .Sum();

        return mainBalanceSum - balanceAccountTransferFrom;
    }

    private decimal GetPendingBalance(int accountId)
    {
        var mainBalanceSum = Context.Transactions
            .AsNoTracking()
            .Where(x =>
                x.ClientId == _currentUser.ClientId
                && x.Status == TransactionStatus.Pending
                && (
                    (x.Type == TransactionType.Category && x.FromAccountId == accountId)
                    || (x.Type == TransactionType.BalanceAdjustment && x.ToAccountId == accountId)
                    || (x.Type == TransactionType.AccountTransfer && x.ToAccountId == accountId)
                ))
            .Select(x => x.Amount)
            .ToList()
            .Sum();
        
        var balanceAccountTransferFrom = Context.Transactions
            .AsNoTracking()
            .Where(x =>
                x.ClientId == _currentUser.ClientId
                && x.Status == TransactionStatus.Pending
                && x.Type == TransactionType.AccountTransfer
                && x.FromAccountId == accountId)
            .Select(x => x.Amount)
            .ToList()
            .Sum();

        return mainBalanceSum - balanceAccountTransferFrom;
    }

    private decimal GetSettledBalance(int accountId)
    {
        var mainBalanceSum = Context.Transactions
            .AsNoTracking()
            .Where(x =>
                x.ClientId == _currentUser.ClientId
                && x.Status == TransactionStatus.Settled
                && (
                    (x.Type == TransactionType.Category && x.FromAccountId == accountId)
                    || (x.Type == TransactionType.BalanceAdjustment && x.ToAccountId == accountId)
                    || (x.Type == TransactionType.AccountTransfer && x.ToAccountId == accountId)
                ))
            .Select(x => x.Amount)
            .ToList()
            .Sum();
        
        var balanceAccountTransferFrom = Context.Transactions
            .AsNoTracking()
            .Where(x =>
                x.ClientId == _currentUser.ClientId
                && x.Status == TransactionStatus.Settled
                && x.Type == TransactionType.AccountTransfer
                && x.FromAccountId == accountId)
            .Select(x => x.Amount)
            .ToList()
            .Sum();

        return mainBalanceSum - balanceAccountTransferFrom;
    }
}