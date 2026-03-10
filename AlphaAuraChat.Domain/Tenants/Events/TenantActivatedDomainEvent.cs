using AlphaAuraChat.Domain.Abstractions;

namespace AlphaAuraChat.Domain.Tenants.Events;

public sealed record TenantActivatedDomainEvent(Guid tenantId) : IDomainEvent;
