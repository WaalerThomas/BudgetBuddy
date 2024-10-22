using BudgetBuddy.Account.Repository;
using BudgetBuddy.Account.Service;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetBuddy.Account;

public static class DependencyInjection
{
    public static IServiceCollection AddAccount(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        return services;
    }
}