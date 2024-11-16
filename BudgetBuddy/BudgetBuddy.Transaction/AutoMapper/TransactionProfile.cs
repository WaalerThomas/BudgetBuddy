using AutoMapper;
using BudgetBuddy.Contracts.Model.Transaction;
using BudgetBuddy.Transaction.Model;
using BudgetBuddy.Transaction.Request;
using BudgetBuddy.Transaction.ViewModel;

namespace BudgetBuddy.Transaction.AutoMapper;

public class TransactionProfile : Profile
{
    public TransactionProfile()
    {
        CreateMap<TransactionModel, TransactionVm>()
            .ReverseMap();
        
        CreateMap<TransactionModel, TransactionDao>()
            .ReverseMap();
        
        CreateMap<CreateTransactionRequest, TransactionModel>();
    }
}