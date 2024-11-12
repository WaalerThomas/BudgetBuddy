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
            var result = _configuration["JwtKey"];
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
            var result = _configuration["JwtIssuer"];
            if (result is null)
            {
                throw new BuddyException("JwtIssuer missing from configuration");
            }
            
            return result;
        }
    }
}