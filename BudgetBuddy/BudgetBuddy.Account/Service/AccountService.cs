using AutoMapper;
using BudgetBuddy.Account.Operations;
using BudgetBuddy.Account.Repositories;
using BudgetBuddy.Contracts.Model.Account;
using BudgetBuddy.Core.Operation;
using BudgetBuddy.Core.Service;

namespace BudgetBuddy.Account.Service;

public class AccountService : ServiceBase, IAccountService
{
    private readonly IMapper _mapper;
    private readonly IAccountRepository _accountRepository;
    
    public AccountService(
        IOperationFactory operationFactory,
        IAccountRepository accountRepository,
        IMapper mapper) : base(operationFactory)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
    }

    public AccountModel Create(AccountModel account)
    {
        var operation = CreateOperation<CreateAccountOperation>();
        return operation.Operate(account);
    }

    public AccountModel? Get(int id)
    {
        var accountModel = _accountRepository.GetById(id);
        return _mapper.Map<AccountModel>(accountModel);
    }

    public IEnumerable<AccountModel> Get()
    {
        var operation = CreateOperation<GetAllAccountsOperation>();
        return operation.Operate();
    }

    public AccountModel Update(AccountModel account)
    {
        var operation = CreateOperation<UpdateAccountOperation>();
        return operation.Operate(account);
    }
}