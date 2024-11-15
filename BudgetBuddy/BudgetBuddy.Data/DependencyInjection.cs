using BudgetBuddy.Account.Repositories;
using BudgetBuddy.Category.Repositories;
using BudgetBuddy.Client.Repositories;
using BudgetBuddy.Data.Repositories;
using BudgetBuddy.Transaction.Repositories;
using CategoryTransfer.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetBuddy.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddData(this IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICategoryTransferRepository, CategoryTransferRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        services.AddScoped<DatabaseContext>();
        
        return services;
    }
}