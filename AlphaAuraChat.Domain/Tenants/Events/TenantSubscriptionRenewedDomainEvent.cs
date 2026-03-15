using AlphaAuraChat.Domain.Abstractions;

namespace AlphaAuraChat.Domain.Tenants.Events;

public sealed record TenantSubscriptionRenewedDomainEvent(Guid tenantId) : IDomainEvent;

