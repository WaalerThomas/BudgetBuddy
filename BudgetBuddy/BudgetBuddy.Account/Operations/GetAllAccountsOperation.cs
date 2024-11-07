using BudgetBuddy.Account.Repositories;
using BudgetBuddy.Contracts.Model.Account;
using BudgetBuddy.Core.Operation;

namespace BudgetBuddy.Account.Operations;

public class GetAllAccountsOperation : Operation<object, IEnumerable<AccountModel>>
{
    private readonly IAccountRepository _accountRepository;

    public GetAllAccountsOperation(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    protected override IEnumerable<AccountModel> OnOperate(object request)
    {
        // TODO: There should probably be some kind of paging here
        
        var accountModels = _accountRepository.GetAll();
        return accountModels;
    }
}