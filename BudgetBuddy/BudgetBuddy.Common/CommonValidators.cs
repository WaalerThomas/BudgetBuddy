using BudgetBuddy.Contracts.Interface.Common;
using BudgetBuddy.Core.Exceptions;

namespace BudgetBuddy.Common;

public class CommonValidators : ICommonValidators
{
    public bool ClientExists(int clientId, bool throwException = false)
    {
        // NOTE: How I want to implement this method
        // var result = _clientService.Get(clientId);
        // if (throwException && result == null)
        // {
        //     throw new BuddyException("Client not found");
        // }
        // return result != null;

        return true;
        throw new NotImplementedException();
    }
}