namespace BudgetBuddy.Core.Exceptions;

public class BuddyException : Exception
{
    public BuddyException(string message) : base(message)
    {
    }
    
    public BuddyException(string message, Exception innerException) : base(message, innerException)
    {
    }
}