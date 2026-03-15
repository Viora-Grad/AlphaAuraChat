using System.Security.Cryptography;
using System.Text;

namespace AlphaAuraChat.Domain.Tenants.Services;

/// <summary>
/// Service responsible for generating unique tenant keys.
/// These keys are not primary keys in the database, but serve as 
/// external identifiers for customers. The service ensures that 
/// each tenant receives a random, unique key that can be used 
/// for tracking, integration, or customer-facing operations.
/// </summary>


public class KeyGenerationService : IKeyGenerationService
{
    public string GenerateKey()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var data = new byte[50];

        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(data);

        var result = new StringBuilder();

        foreach (var b in data)
        {
            result.Append(chars[b % chars.Length]);
        }

        return result.ToString();
    }
}

