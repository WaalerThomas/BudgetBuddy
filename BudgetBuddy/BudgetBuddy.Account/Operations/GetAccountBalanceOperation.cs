using AutoMapper;
using BudgetBuddy.Account.Repositories;
using BudgetBuddy.Contracts.Model.Account;
using BudgetBuddy.Core.Operation;

namespace BudgetBuddy.Account.Operations;

public class GetAccountBalanceOperation : Operation<int, AccountBalanceModel>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;

    public GetAccountBalanceOperation(IAccountRepository accountRepository, IMapper mapper)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
    }

    protected override AccountBalanceModel OnOperate(int request)
    {
        throw new NotImplementedException();
    }
}