namespace BudgetBuddy.Core.Operation;

public interface IOperationAsyncBase
{
    Task<object> OperateAsync(object request);
}

public interface IOperationAsync<in TRequest> : IOperationAsyncBase
{
    Task OperateAsync(TRequest request = default(TRequest));
}

public interface IOperationAsync<in TRequest, out TResponse> : IOperationAsyncBase
{
    TResponse OperateAsync(TRequest request = default(TRequest));
}