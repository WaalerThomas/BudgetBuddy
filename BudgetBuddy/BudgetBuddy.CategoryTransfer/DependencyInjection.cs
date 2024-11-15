using BudgetBuddy.Contracts.Interface.CategoryTransfer;
using CategoryTransfer.AutoMapper;
using CategoryTransfer.Operations;
using CategoryTransfer.Service;
using Microsoft.Extensions.DependencyInjection;

namespace CategoryTransfer;

public static class DependencyInjection
{
    public static IServiceCollection AddCategoryTransfer(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(CategoryTransferProfile));

        services.AddScoped<ICategoryTransferValidator, CategoryTransferValidator>();
        services.AddScoped<ICategoryTransferService, CategoryTransferService>();
        
        services.AddOperations();

        return services;
    }

    private static void AddOperations(this IServiceCollection services)
    {
        services.AddScoped<CreateCategoryTransferOperation>();
        services.AddScoped<GetAvailableToBudgetOperation>();
    }
}