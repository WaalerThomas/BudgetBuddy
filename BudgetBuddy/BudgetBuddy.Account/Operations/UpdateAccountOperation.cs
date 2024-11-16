using AutoMapper;
using BudgetBuddy.Account.Model;
using BudgetBuddy.Account.Repositories;
using BudgetBuddy.Account.Service;
using BudgetBuddy.Contracts.Model.Account;
using BudgetBuddy.Core.Operation;
using FluentValidation;

namespace BudgetBuddy.Account.Operations;

public class UpdateAccountOperation : Operation<AccountModel, AccountModel>
{
    private readonly IMapper _mapper;
    private readonly IAccountRepository _accountRepository;
    private readonly IAccountValidator _accountValidator;

    public UpdateAccountOperation(
        IAccountRepository accountRepository,
        IAccountValidator accountValidator,
        IMapper mapper)
    {
        _accountRepository = accountRepository;
        _accountValidator = accountValidator;
        _mapper = mapper;
    }

    protected override AccountModel OnOperate(AccountModel accountModel)
    {
        _accountValidator.ValidateAndThrow(accountModel);
        _accountValidator.ValidateNameUniqueness(accountModel);
        
        var accountDao = _mapper.Map<AccountDao>(accountModel);
        accountDao = _accountRepository.Update(accountDao);
        
        accountModel = _mapper.Map<AccountModel>(accountDao);
        return accountModel;
    }
}