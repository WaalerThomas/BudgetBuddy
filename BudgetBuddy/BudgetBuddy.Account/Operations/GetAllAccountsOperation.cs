using AutoMapper;
using BudgetBuddy.Account.Model;
using BudgetBuddy.Account.Repositories;
using BudgetBuddy.Contracts.Model.Account;
using BudgetBuddy.Core.Operation;

namespace BudgetBuddy.Account.Operations;

public class GetAllAccountsOperation : Operation<object, IEnumerable<AccountModel>>
{
    private readonly IMapper _mapper;
    private readonly IAccountRepository _accountRepository;

    public GetAllAccountsOperation(IAccountRepository accountRepository, IMapper mapper)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
    }

    protected override IEnumerable<AccountModel> OnOperate(object request)
    {
        var accountDaoList = _accountRepository.GetAll();
        var accountModels =_mapper.Map<IEnumerable<AccountDao>, IEnumerable<AccountModel>>(accountDaoList);
        return accountModels;
    }
}