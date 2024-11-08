using BudgetBuddy.Account.AutoMapper;
using BudgetBuddy.Account.Operations;
using BudgetBuddy.Account.Repositories;
using BudgetBuddy.Account.Service;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetBuddy.Account;

public static class DependencyInjection
{
    public static IServiceCollection AddAccount(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AccountProfile));
        
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IAccountValidator, AccountValidator>();
        
        services.AddScoped<CreateAccountOperation>();
        services.AddScoped<GetAllAccountsOperation>();
        services.AddScoped<UpdateAccountOperation>();
        
        return services;
    }
}