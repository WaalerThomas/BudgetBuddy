using Microsoft.Extensions.DependencyInjection;

namespace BudgetBuddy.Core.Operation;

public class OperationFactory : IOperationFactory
{
    private readonly IServiceProvider _serviceProvider;

    public OperationFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public T Create<T>() where T : class, IOperationBase
    {
        return _serviceProvider.GetRequiredService<T>();
    }

    public T CreateAsync<T>() where T : class, IOperationAsyncBase
    {
        return _serviceProvider.GetRequiredService<T>();
    }
}