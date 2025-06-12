namespace BudgetBuddy.Contracts.Response.Client;

public record AuthenticationTokensResponse(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresIn,
    DateTime RefreshExpiresIn
);