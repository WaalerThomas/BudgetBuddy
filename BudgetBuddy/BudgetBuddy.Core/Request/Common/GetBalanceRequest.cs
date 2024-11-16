namespace BudgetBuddy.Core.Request.Common;

public class GetBalanceRequest
{
    public int Id { get; set; }
    public bool OnlyActualBalance { get; set; }
}