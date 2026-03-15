namespace AlphaAuraChat.Domain.Tenants.Internal;

public record Subscription(Guid PlanId, DateTime ActiviationTimeUtc, DateTime ExpiryTimeUtc, Status Status)
{
    public static Subscription CreateDefaultSubscription()
    {
        return new Subscription(Guid.Empty, DateTime.UtcNow, DateTime.UtcNow.AddDays(7), Status.InActive);
    }
}