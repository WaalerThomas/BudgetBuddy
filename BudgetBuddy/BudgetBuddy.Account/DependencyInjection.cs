using BudgetBuddy.Account.AutoMapper;
using BudgetBuddy.Account.Operations;
using BudgetBuddy.Account.Repositories;
using BudgetBuddy.Account.Service;
using BudgetBuddy.Contracts.Interface.Account;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetBuddy.Account;

public static class DependencyInjection
{
    public static IServiceCollection AddAccount(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AccountProfile));
        
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IAccountValidator, AccountValidator>();
        
        services.AddOperations();
        
        return services;
    }
    
    private static void AddOperations(this IServiceCollection services)
    {
        services.AddScoped<CreateAccountOperation>();
        services.AddScoped<GetAllAccountsOperation>();
        services.AddScoped<UpdateAccountOperation>();
        services.AddScoped<GetAccountBalanceOperation>();
    }
}