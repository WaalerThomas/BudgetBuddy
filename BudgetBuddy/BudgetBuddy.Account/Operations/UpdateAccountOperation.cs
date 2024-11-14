using AutoMapper;
using BudgetBuddy.Account.Model;
using BudgetBuddy.Account.Repositories;
using BudgetBuddy.Account.Resources;
using BudgetBuddy.Account.Service;
using BudgetBuddy.Contracts.Model.Account;
using BudgetBuddy.Core.Exceptions;
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
        
        // Account either does not exist or does not belong to the user when null
        var existingAccountDao = _accountRepository.GetById(accountModel.Id);
        if (existingAccountDao is null)
        {
            throw new BuddyException(AccountResource.AccountNotFound);
        }
        
        _accountValidator.ValidateNameUniqueness(accountModel);
        
        var accountDao = _mapper.Map<AccountDao>(accountModel);
        
        // Set fields that are not allowed to be updated
        accountDao.Id = existingAccountDao.Id;
        accountDao.ClientId = existingAccountDao.ClientId;
        accountDao.CreatedAt = existingAccountDao.CreatedAt;
        accountDao.UpdatedAt = existingAccountDao.UpdatedAt;
        
        accountDao = _accountRepository.Update(accountDao);
        
        accountModel = _mapper.Map<AccountModel>(accountDao);
        return accountModel;
    }
}