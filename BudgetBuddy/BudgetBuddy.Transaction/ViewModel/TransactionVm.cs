﻿using BudgetBuddy.Contracts.Enums;

namespace BudgetBuddy.Transaction.ViewModel;

public class TransactionVm
{
    public int Id { get; set; }
    public Guid ClientId { get; set; }
    public DateOnly Date { get; set; }
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public int? CategoryId { get; set; }
    public int? FromAccountId { get; set; }
    public int? ToAccountId { get; set; }
    public string Memo { get; set; } = string.Empty;
    public TransactionStatus Status { get; set; }
}