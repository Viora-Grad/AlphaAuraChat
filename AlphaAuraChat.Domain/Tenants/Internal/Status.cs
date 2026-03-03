namespace AlphaAuraChat.Domain.Tenants.Internal;

public record Status(string Value)
{
    public readonly static Status Active = new(nameof(Active));
    public readonly static Status Suspended = new(nameof(Suspended));
    public readonly static Status Expired = new(nameof(Expired));
}
