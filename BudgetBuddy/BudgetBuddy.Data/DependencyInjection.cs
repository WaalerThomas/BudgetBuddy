﻿using BudgetBuddy.Account.Repositories;
using BudgetBuddy.Category.Repositories;
using BudgetBuddy.Client.Repositories;
using BudgetBuddy.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetBuddy.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddData(this IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        
        return services;
    }
}