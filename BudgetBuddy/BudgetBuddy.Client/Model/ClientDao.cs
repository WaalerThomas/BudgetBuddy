using BudgetBuddy.Core.Database;

namespace BudgetBuddy.Client.Model;

public class ClientDao : BuddyDao
{
    public new Guid Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public byte[]? Salt { get; set; }
}