using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Application.Repositories;
using BudgetBuddy.Application.Services;
using BudgetBuddy.Domain.Services;
using BudgetBuddy.Infrastructure;
using BudgetBuddy.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static void Main(string[] args)
    {
        var services = CreateServices();

        Application application = services.GetRequiredService<Application>();
        application.Run();
    }

    private static ServiceProvider CreateServices()
    {
        var servicesProvider = new ServiceCollection()
            .AddTransient<IUnitOfWork, UnitOfWork>()
            .AddTransient<ICategoryGroupRepository, CategoryGroupRepository>()
            .AddTransient<ICategoryGroupService, CategoryGroupService>()
            .AddSingleton<Application>()
            .BuildServiceProvider();

        return servicesProvider;
    }
}

public class Application
{
    private readonly ICategoryGroupService _categoryGroupService;

    public Application(ICategoryGroupService categoryGroupService)
    {
        _categoryGroupService = categoryGroupService;
    }

    public void Run()
    {
        Console.WriteLine("We are running the application");
        _categoryGroupService.CreateCategoryGroup("other thing");
        var result = _categoryGroupService.CreateCategoryGroup("something");
        if (result.IsSuccess)
            Console.WriteLine($"CategoryGroup Id: {result.Value}");
        else
            Console.WriteLine($"Failed to create CategoryGroup: {result.Error}");
    }
}