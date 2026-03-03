namespace AlphaAuraChat.Domain.Tenants.Internal;

public record Subscription(Guid PlanId, DateTime ActiviationTimeUtc, DateTime ExpiryTimeUtc, Status Status);