using AutoMapper;
using BudgetBuddy.Contracts.Model.Common;
using BudgetBuddy.Contracts.Model.Transaction;
using BudgetBuddy.Transaction.Request;
using BudgetBuddy.Transaction.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BudgetBuddy.Transaction.Controller;

[ApiController]
[Route("api/transactions")]
public class TransactionController
{
    private readonly IMapper _mapper;

    public TransactionController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpPost]
    public BuddyResponse<TransactionVm> Create(CreateTransactionRequest createTransactionRequest)
    {
        var transactionModel = _mapper.Map<TransactionModel>(createTransactionRequest);
        throw new NotImplementedException();
    }
}