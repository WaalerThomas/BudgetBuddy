using BudgetBuddy.Account.Repositories;
using BudgetBuddy.Account.Service;
using BudgetBuddy.Common.Database;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetBuddy.Account;

public static class DependencyInjection
{
    public static IServiceCollection AddAccount(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IAccountValidator, AccountValidator>();
        
        services.AddScoped<DatabaseContext, DatabaseContext>(); // TODO: Will this work?
        
        return services;
    }
}