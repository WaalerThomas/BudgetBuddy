﻿using BudgetBuddy.Contracts.Enums;

namespace BudgetBuddy.Category.ViewModel;

public class CategoryVm
{
    public int Id { get; set; }
    public Guid ClientId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal? MonthlyAmount { get; set; }
    public decimal? GoalAmount { get; set; }
    public CategoryType Type { get; set; }
    public int? GroupId { get; set; }
}