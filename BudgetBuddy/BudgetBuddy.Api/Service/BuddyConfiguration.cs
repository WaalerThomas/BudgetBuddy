using BudgetBuddy.Common.Service;
using BudgetBuddy.Core.Exceptions;

namespace BudgetBuddy.Api.Service;

public class BuddyConfiguration : IBuddyConfiguration
{
    private readonly IConfiguration _configuration;

    public BuddyConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string JwtKey
    {
        get
        {
            var result = _configuration["Jwt:Key"];
            if (result is null)
            {
                throw new BuddyException("JwtKey missing from configuration");
            }
            
            return result;
        }
    }

    public string JwtIssuer
    {
        get
        {
            var result = _configuration["Jwt:Issuer"];
            if (result is null)
            {
                throw new BuddyException("JwtIssuer missing from configuration");
            }
            
            return result;
        }
    }
    
    public string Pepper
    {
        get
        {
            var result = _configuration["Pepper"];
            if (result is null)
            {
                throw new BuddyException("Pepper missing from configuration");
            }
            
            return result;
        }
    }

    public int KeySize
    {
        get
        {
            var result = _configuration["KeySize"];
            if (result is null)
            {
                throw new BuddyException("KeySize missing from configuration");
            }
            
            return int.Parse(result);
        }
    }

    public int Iterations
    {
        get
        {
            var result = _configuration["Iterations"];
            if (result is null)
            {
                throw new BuddyException("Iterations missing from configuration");
            }
            
            return int.Parse(result);
        }
    }
}