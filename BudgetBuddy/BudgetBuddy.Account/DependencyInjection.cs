using BudgetBuddy.Account.AutoMapper;
using BudgetBuddy.Account.Operations;
using BudgetBuddy.Account.Repositories;
using BudgetBuddy.Account.Request;
using BudgetBuddy.Account.Service;
using BudgetBuddy.Common.Database;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetBuddy.Account;

public static class DependencyInjection
{
    public static IServiceCollection AddAccount(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AccountProfile));
        
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IAccountValidator, AccountValidator>();
        
        services.AddScoped<DatabaseContext, DatabaseContext>(); // TODO: Will this work?

        services.AddScoped<UpdateAccountOperation>();
        
        return services;
    }
}