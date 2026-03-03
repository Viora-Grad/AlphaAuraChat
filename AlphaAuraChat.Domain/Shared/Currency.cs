namespace AlphaAuraChat.Domain.Shared;

public record Currency
{
    public static readonly Currency Usd = new("USD");
    public static readonly Currency Egp = new("EGP");
    public string Code { get; init; }
    private Currency(string code)
    {
        Code = code;
    }
}
