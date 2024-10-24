namespace BudgetBuddy.Contracts.Interface.Common;

public interface ICommonValidators
{
    bool ClientExists(int clientId, bool throwException = false);
}