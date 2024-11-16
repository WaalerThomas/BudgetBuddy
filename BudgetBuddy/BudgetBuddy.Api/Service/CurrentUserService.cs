using BudgetBuddy.Common.Service;
using BudgetBuddy.Core.Exceptions;

namespace BudgetBuddy.Api.Service;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid ClientId
    {
        get
        {
            var value = _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(c => c.Type == "client_id")
                ?.Value;

            if (value is null)
            {
                throw new BuddyException("Client id missing from claims");
            }
            
            return Guid.Parse(value);
        }
    }

    public string Username
    {
        get
        {
            var value = _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(c => c.Type == "username")
                ?.Value;

            if (value is null)
            {
                throw new BuddyException("Username missing from claims");
            }

            return value;
        }
    }
}