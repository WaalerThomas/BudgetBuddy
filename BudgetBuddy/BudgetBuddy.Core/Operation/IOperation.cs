namespace BudgetBuddy.Core.Operation;

public interface IOperationBase
{
    object Operate(object request);
}

public interface IOperation : IOperationBase
{
    void Operate();
}

public interface IOperation<in TRequest> : IOperationBase
{
    void Operate(TRequest request = default(TRequest));
}

public interface IOperation<in TRequest, out TResponse> : IOperationBase
{
    TResponse Operate(TRequest request = default(TRequest));
}