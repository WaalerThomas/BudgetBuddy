namespace BudgetBuddy.Contracts.Model.Account;

public class AccountModel
{
    public int Id { get; set; }
    public required string Name { get; init; }
    public required string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int ClientId { get; set; }
}