using AlphaAuraChat.Domain.Abstractions;

namespace AlphaAuraChat.Domain.Plans.Events;

public sealed record PlanSuspendedEvent(Guid PlanId) : IDomainEvent;

