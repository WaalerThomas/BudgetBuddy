using BudgetBuddy.Category.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetBuddy.Category;

public static class DependencyInjection
{
    public static IServiceCollection AddCategory(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(CategoryProfile));
        
        return services;
    }
}