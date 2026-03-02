using AlphaAuraChat.Domain.Abstractions;

namespace AlphaAuraChat.Domain.Tenants;

public static class TenantErrors
{
    public static readonly Error ContactsMissing = new("Tenant.MissingContact", "One contact atleast should be provided");
}
