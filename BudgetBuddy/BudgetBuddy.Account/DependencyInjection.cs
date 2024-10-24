using BudgetBuddy.Account.Repositories;
using BudgetBuddy.Account.Service;
using BudgetBuddy.Common.Database;
using BudgetBuddy.Contracts.Model.Account;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetBuddy.Account;

public static class DependencyInjection
{
    public static IServiceCollection AddAccount(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IValidator<AccountModel>, AccountValidator>();
        
        services.AddScoped<DatabaseContext, DatabaseContext>(); // TODO: Will this work?
        
        return services;
    }
}