using BudgetBuddy.Core.Operation;

namespace BudgetBuddy.Core.Service;

public abstract class ServiceBase : IService
{
    private readonly IOperationFactory _operationFactory;

    protected ServiceBase(IOperationFactory operationFactory)
    {
        _operationFactory = operationFactory;
    }
    
    protected TOperation CreateOperation<TOperation>()
        where TOperation : class, IOperationBase
    {
        return _operationFactory.Create<TOperation>();
    }
    
    protected TAsyncOperation CreateAsyncOperation<TAsyncOperation>()
        where TAsyncOperation : class, IOperationAsyncBase
    {
        return _operationFactory.CreateAsync<TAsyncOperation>();
    }
}