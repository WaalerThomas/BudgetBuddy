using AutoMapper;
using BudgetBuddy.Contracts.Model.Common;
using BudgetBuddy.Contracts.Model.Transaction;
using BudgetBuddy.Transaction.Request;
using BudgetBuddy.Transaction.Service;
using BudgetBuddy.Transaction.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetBuddy.Transaction.Controller;

[Authorize]
[ApiController]
[Route("api/transactions")]
public class TransactionController
{
    private readonly IMapper _mapper;
    private readonly ITransactionService _transactionService;

    public TransactionController(IMapper mapper, ITransactionService transactionService)
    {
        _mapper = mapper;
        _transactionService = transactionService;
    }

    [HttpPost]
    public BuddyResponse<TransactionVm> Create(CreateTransactionRequest createTransactionRequest)
    {
        var transactionModel = _mapper.Map<TransactionModel>(createTransactionRequest);
        transactionModel = _transactionService.Create(transactionModel);
        return new BuddyResponse<TransactionVm>(_mapper.Map<TransactionVm>(transactionModel));
    }
}