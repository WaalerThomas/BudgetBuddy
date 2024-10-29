namespace BudgetBuddy.Client.Request;

public class CreateClientRequest
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string ControlPassword { get; set; }
}