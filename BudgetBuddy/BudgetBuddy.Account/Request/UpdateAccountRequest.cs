namespace BudgetBuddy.Account.Request;

public class UpdateAccountRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}