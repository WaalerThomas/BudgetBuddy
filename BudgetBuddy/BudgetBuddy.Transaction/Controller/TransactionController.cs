using AutoMapper;
using BudgetBuddy.Contracts.Model.Common;
using BudgetBuddy.Contracts.Model.Transaction;
using BudgetBuddy.Core.Exceptions;
using BudgetBuddy.Transaction.Request;
using BudgetBuddy.Transaction.Service;
using BudgetBuddy.Transaction.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [EndpointSummary("Create a new transaction")]
    public BuddyResponse<TransactionVm> Create(CreateTransactionRequest createTransactionRequest)
    {
        var transactionModel = _mapper.Map<TransactionModel>(createTransactionRequest);
        transactionModel = _transactionService.Create(transactionModel);
        return new BuddyResponse<TransactionVm>(_mapper.Map<TransactionVm>(transactionModel));
    }
    
    [HttpGet]
    [EndpointSummary("Get all transactions")]
    public BuddyResponse<IEnumerable<TransactionVm>> Get()
    {
        var transactions = _transactionService.Get();
        return new BuddyResponse<IEnumerable<TransactionVm>>(_mapper.Map<IEnumerable<TransactionVm>>(transactions));
    }

    [HttpPatch]
    [EndpointSummary("Update a transaction")]
    public BuddyResponse<TransactionVm> Update(UpdateTransactionRequest updateTransactionRequest)
    {
        var transactionModel = _transactionService.GetById(updateTransactionRequest.Id);
        if (transactionModel == null)
        {
            throw new BuddyException("Transaction not found");
        }

        _mapper.Map(updateTransactionRequest, transactionModel);
        
        transactionModel = _transactionService.Update(transactionModel);
        return new BuddyResponse<TransactionVm>(_mapper.Map<TransactionVm>(transactionModel));
    }
}