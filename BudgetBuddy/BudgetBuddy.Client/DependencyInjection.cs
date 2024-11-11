using BudgetBuddy.Client.AutoMapper;
using BudgetBuddy.Client.Operations;
using BudgetBuddy.Client.Repositories;
using BudgetBuddy.Client.Service;
using BudgetBuddy.Contracts.Interface.Client;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetBuddy.Client;

public static class DependencyInjection
{
    public static IServiceCollection AddClient(this IServiceCollection services)
    {
        // TODO: Add Authentication
        services.AddAutoMapper(typeof(ClientProfile));

        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IClientValidator, ClientValidator>();
        services.AddScoped<IPasswordService, PasswordService>();

        services.AddScoped<LoginClientOperation>();
        services.AddScoped<CreateClientOperation>();
        services.AddScoped<GetClientByUsernameOperation>();
        
        return services;
    }
}