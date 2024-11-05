using BudgetBuddy.Common.Service;

namespace BudgetBuddy.Api.Service;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        ClientId = Guid.Parse(_httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "client_id")?.Value ?? string.Empty);
        Username = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "username")?.Value ?? string.Empty;
    }

    public Guid ClientId { get; }
    public string Username { get; }
}