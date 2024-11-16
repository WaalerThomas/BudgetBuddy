namespace BudgetBuddy.Common.Service;

public interface ICurrentUserService
{
    Guid ClientId { get; }
    string Username { get; }
}