using BudgetBuddy.Transaction.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetBuddy.Transaction;

public static class DependencyInjection
{
    public static IServiceCollection AddTransaction(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(TransactionProfile));
        
        return services;
    }
}