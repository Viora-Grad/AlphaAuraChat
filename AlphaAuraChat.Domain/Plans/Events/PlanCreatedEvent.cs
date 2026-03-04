using AlphaAuraChat.Domain.Abstractions;

namespace AlphaAuraChat.Domain.Plans.Events;

public sealed record PlanCreatedEvent(Guid PlanId) : IDomainEvent;


