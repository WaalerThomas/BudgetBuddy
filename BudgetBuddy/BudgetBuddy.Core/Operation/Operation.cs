namespace BudgetBuddy.Core.Operation;

public abstract class Operation<TRequest, TResponse> : IOperation<TRequest, TResponse>
{
    public virtual TResponse Operate(TRequest request = default(TRequest))
    {
        var response = OnOperate(request);

        return response;
    }

    object IOperationBase.Operate(object request)
    {
        return Operate((TRequest)request);
    }

    protected abstract TResponse OnOperate(TRequest request);
}