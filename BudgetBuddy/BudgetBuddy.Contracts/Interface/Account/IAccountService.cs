using BudgetBuddy.Contracts.Model.Account;

namespace BudgetBuddy.Contracts.Interface.Account;

public interface IAccountService
{
    AccountModel Create(AccountModel account);
    AccountModel? Get(int id);
    IEnumerable<AccountModel> Get();
    AccountModel Update(AccountModel account);

    AccountBalanceModel GetBalance(int id, bool onlyActualBalance = false);
}