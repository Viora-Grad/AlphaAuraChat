namespace AlphaAuraChat.Domain.Tenants;

public class KeyGenerationService
{
    public string GenerateKey()
    {
        return Guid.NewGuid().ToString("N");
    }
}
