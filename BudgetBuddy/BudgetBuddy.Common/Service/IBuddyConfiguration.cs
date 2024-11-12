namespace BudgetBuddy.Common.Service;

public interface IBuddyConfiguration
{
    public string JwtKey { get; }
    public string JwtIssuer { get; }
    public string Pepper { get; }
}