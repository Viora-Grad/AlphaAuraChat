using AlphaAuraChat.Domain.Abstractions;

namespace AlphaAuraChat.Domain.Plans.Events;

public sealed record PlanDeactivatedEvent(Guid PlanId) : IDomainEvent;

