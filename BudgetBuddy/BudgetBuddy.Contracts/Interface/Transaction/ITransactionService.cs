using BudgetBuddy.Contracts.Model.Account;
using BudgetBuddy.Contracts.Model.Transaction;

namespace BudgetBuddy.Contracts.Interface.Transaction;

public interface ITransactionService
{
    TransactionModel Create(TransactionModel transactionModel);
    IEnumerable<TransactionModel> Get();
    TransactionModel? GetById(int id);
    TransactionModel Update(TransactionModel transactionModel);
    
    AccountBalanceModel GetBalance(int id, bool onlyActualBalance = false);
}