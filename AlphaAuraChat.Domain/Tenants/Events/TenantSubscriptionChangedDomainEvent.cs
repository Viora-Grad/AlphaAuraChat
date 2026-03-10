using AlphaAuraChat.Domain.Abstractions;

namespace AlphaAuraChat.Domain.Tenants.Events;

public sealed record TenantSubscriptionChangedDomainEvent(Guid tenantId) : IDomainEvent;
