using BudgetBuddyCore.Models;

namespace BudgetBuddyCore.Interfaces;

public interface IAccountRespository
{
    IEnumerable<Account> GetAccounts();
    Account? GetAccount(int id);
    Account? GetAccountByName(string name);
    bool Insert(Account account);
    bool Delete(int id);
    bool Update(Account account);
}