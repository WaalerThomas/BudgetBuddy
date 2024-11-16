namespace BudgetBuddy.Core.Operation;

public interface IOperationFactory
{
    T Create<T>() where T : class, IOperationBase;
    T CreateAsync<T>() where T : class, IOperationAsyncBase;
}