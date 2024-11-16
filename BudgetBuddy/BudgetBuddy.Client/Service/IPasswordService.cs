namespace BudgetBuddy.Client.Service;

public interface IPasswordService
{
    string Hash(string password, out byte[] salt);
    bool Verify(string password, string hash, byte[] salt);
}