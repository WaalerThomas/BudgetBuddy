namespace BudgetBuddy.Models;

public class Category
{
    private const int MIN_NAME_LENGTH = 4;      // TODO: Move these length values in a common spot. Used in multiple places
    private const int MAX_NAME_LENGTH = 25;

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
    
    public decimal MonthlyAmount { get; set; }
    public decimal GoalAmount { get; set; }
    public Group? Group { get; set; }   // FIXME: Need to enforce that a category need to be a part of a group
}