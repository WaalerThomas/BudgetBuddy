namespace BudgetBuddy.Client.Request;

public class LoginClientRequest
{
    public string? Username { get; set; }
    public string? Password { get; set; }
    public bool RememberMe { get; set; }
}