﻿using AutoMapper;
using BudgetBuddy.Common.Database;
using BudgetBuddy.Common.Repositories;
using BudgetBuddy.Common.Service;
using BudgetBuddy.Contracts.Model.Account;

namespace BudgetBuddy.Account.Repositories;

public class AccountRepository : Repository<AccountModel>, IAccountRepository
{
    private readonly IMapper _mapper;

    public AccountRepository(
        DatabaseContext context,
        IMapper mapper,
        ICurrentUserService currentUserService) : base(context, currentUserService)
    {
        _mapper = mapper;
    }
    
    public AccountModel Create(AccountModel model)
    {
        // TODO: What happens when saving fails??
        
        model.ClientId = _currentUser.ClientId;
        model.CreatedAt = DateTime.UtcNow;
        
        var result = Context.Accounts.Add(model);
        
        Context.SaveChanges();
        
        return result.Entity;
    }

    public Task<AccountModel> CreateAsync(AccountModel model)
    {
        throw new NotImplementedException();
    }

    public AccountModel Update(AccountModel model)
    {
        throw new NotImplementedException();
    }

    public Task<AccountModel> UpdateAsync(AccountModel model)
    {
        throw new NotImplementedException();
    }

    public void Delete(AccountModel model)
    {
        throw new NotImplementedException();
    }

    public AccountModel GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<AccountModel> GetByIds(IEnumerable<int> ids)
    {
        throw new NotImplementedException();
    }

    public override IEnumerable<AccountModel> GetAll()
    {
        return Context.Accounts
            .Where(x => x.ClientId == _currentUser.ClientId)
            .ToList();
    }
}