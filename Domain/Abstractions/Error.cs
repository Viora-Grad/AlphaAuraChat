namespace AlphaAuraChat.Domain.Abstractions;

public record Error(string Name, string Description)
{
    public static Error NoError => new(string.Empty, string.Empty);
    public static Error NullValue => new("Error.NullValue", "Null value was returned");
}
