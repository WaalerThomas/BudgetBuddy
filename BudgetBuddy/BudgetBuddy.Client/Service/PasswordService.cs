using System.Security.Cryptography;
using System.Text;

namespace BudgetBuddy.Client.Service;

public class PasswordService : IPasswordService
{
    private const int KeySize = 32;
    private const int Iterations = 350000;
    private static readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA512;
    
    public string HashPassword(string password, out byte[] salt)
    {
        salt = RandomNumberGenerator.GetBytes(KeySize);

        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            Iterations,
            HashAlgorithm,
            KeySize);

        return Convert.ToHexString(hash);
    }

    public bool VerifyPassword(string password, string hash, byte[] salt)
    {
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithm, KeySize);

        return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
    }
}