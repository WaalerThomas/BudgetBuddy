﻿using BudgetBuddy.Contracts.Interface.Common;
using BudgetBuddy.Contracts.Model.Account;
using FluentValidation;

namespace BudgetBuddy.Account.Service;

public class AccountValidator : AbstractValidator<AccountModel>, IAccountValidator
{
    private readonly ICommonValidators _commonValidators;
    
    public AccountValidator(ICommonValidators commonValidators)
    {
        _commonValidators = commonValidators;
        
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Type).IsInEnum();
    }
}