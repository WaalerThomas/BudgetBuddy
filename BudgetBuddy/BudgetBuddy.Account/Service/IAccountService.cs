using BudgetBuddy.Account.ViewModel;
using BudgetBuddy.Contracts.Model.Account;

namespace BudgetBuddy.Account.Service;

public interface IAccountService
{
    AccountModel Create(AccountModel account);
}