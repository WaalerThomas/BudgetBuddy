using BudgetBuddy.Transaction.AutoMapper;
using BudgetBuddy.Transaction.Operations;
using BudgetBuddy.Transaction.Service;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetBuddy.Transaction;

public static class DependencyInjection
{
    public static IServiceCollection AddTransaction(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(TransactionProfile));

        services.AddScoped<ITransactionValidator, TransactionValidator>();
        services.AddScoped<ITransactionService, TransactionService>();

        services.AddScoped<CreateTransactionOperation>();
        
        return services;
    }
}