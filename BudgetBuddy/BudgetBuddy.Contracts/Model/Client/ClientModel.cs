namespace BudgetBuddy.Contracts.Model.Client;

public class ClientModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public byte[]? Salt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}