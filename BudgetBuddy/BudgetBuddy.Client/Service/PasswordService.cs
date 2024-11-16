using System.Security.Cryptography;
using System.Text;
using BudgetBuddy.Common.Service;

namespace BudgetBuddy.Client.Service;

public class PasswordService : IPasswordService
{
    private readonly IBuddyConfiguration _buddyConfiguration;

    private static readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA512;
    
    private readonly int _keySize;
    private readonly int _iterations;
    private readonly string _pepper;
    
    public PasswordService(IBuddyConfiguration buddyConfiguration)
    {
        _buddyConfiguration = buddyConfiguration;
        
        _pepper = buddyConfiguration.Pepper;
        _keySize = buddyConfiguration.KeySize;
        _iterations = buddyConfiguration.Iterations;
    }

    public string Hash(string password, out byte[] salt)
    {
        salt = RandomNumberGenerator.GetBytes(_keySize);

        password = _pepper + password;

        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            _iterations,
            HashAlgorithm,
            _keySize);

        return Convert.ToHexString(hash);
    }

    public bool Verify(string password, string hash, byte[] salt)
    {
        password = _pepper + password;
        
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, _iterations, HashAlgorithm, _keySize);

        return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
    }
}