using AlphaAuraChat.Domain.Abstractions;

namespace AlphaAuraChat.Domain.Tenants;

public static class TenantErrors
{
    public static readonly Error ContactsMissing = new("Tenant.MissingContact", "One contact atleast should be provided");
    public static readonly Error AlreadyActivate = new("Tenant.AlreadyActivate", "Tenant is already active.");
    public static readonly Error SamePlan = new("Tenant.AlreadySubscribeToThisPlan", "Tenant is already subscribe to this plan.");
    public static readonly Error InvalidDuration = new("Tenant.InvalidDuration", "Subscription duration should be greater than zero.");

}
