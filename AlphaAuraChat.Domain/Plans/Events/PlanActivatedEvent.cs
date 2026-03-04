using AlphaAuraChat.Domain.Abstractions;

namespace AlphaAuraChat.Domain.Plans.Events;

public sealed record PlanActivatedEvent(Guid PlanId) : IDomainEvent;

