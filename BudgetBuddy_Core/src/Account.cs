namespace BudgetBuddyCore;

public abstract class Account
{
    public void CreateAccount(string name)
    {
        if (Exists(name))
            throw new ArgumentException("Account name is already in use", name);
    }

    public abstract bool Exists(string name);
}