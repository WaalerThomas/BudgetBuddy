using AutoMapper;
using BudgetBuddy.Account.Repositories;
using BudgetBuddy.Contracts.Interface.Transaction;
using BudgetBuddy.Contracts.Model.Account;
using BudgetBuddy.Core.Exceptions;
using BudgetBuddy.Core.Operation;
using BudgetBuddy.Core.Request.Common;

namespace BudgetBuddy.Account.Operations;

public class GetAccountBalanceOperation : Operation<GetBalanceRequest, AccountBalanceModel>
{
    private readonly IAccountRepository _accountRepository;
    private readonly ITransactionService _transactionService;

    public GetAccountBalanceOperation(
        IAccountRepository accountRepository,
        ITransactionService transactionService)
    {
        _accountRepository = accountRepository;
        _transactionService = transactionService;
    }

    protected override AccountBalanceModel OnOperate(GetBalanceRequest request)
    {
        var accountDao = _accountRepository.GetById(request.Id);
        if (accountDao == null)
        {
            throw new BuddyException("Account not found");
        }
        
        var balance = _transactionService.GetBalance(request.Id, request.OnlyActualBalance);
        balance.Id = accountDao.Id;
        return balance;
    }
}