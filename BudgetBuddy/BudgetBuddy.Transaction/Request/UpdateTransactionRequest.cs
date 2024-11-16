namespace BudgetBuddy.Transaction.Request;

public class UpdateTransactionRequest : CreateTransactionRequest
{
    public int Id { get; set; }
}