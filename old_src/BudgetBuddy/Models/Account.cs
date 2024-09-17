namespace BudgetBuddy.Models;

public class Account
{
    public const int MIN_NAME_LENGTH = 4;      // TODO: Move these length values in a common spot. Used in multiple places
    public const int MAX_NAME_LENGTH = 25;

    public int Id { get; set; }
    
    private string _name = "";
    public required string Name {
        get => _name;
        set
        {
            if (value.Length < MIN_NAME_LENGTH)
                throw new ArgumentException($"Name is too short. Minimum {MIN_NAME_LENGTH} characters long.");
            if (value.Length > MAX_NAME_LENGTH)
                throw new ArgumentException($"Name is too long. Maximum {MAX_NAME_LENGTH} characters long.");
            _name = value;
        }
    }

    public decimal SettledBalance { get; set; }
    public decimal PendingBalance { get; set; }
    public DateTime? CalculatedSettledDate { get; set; }
    public DateTime? OldestPendingDate { get; set; }

    public decimal ActualBalance => PendingBalance + SettledBalance;
}