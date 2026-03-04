using AlphaAuraChat.Domain.Abstractions;

namespace AlphaAuraChat.Domain.Plans.Events;

public sealed record PlanCancelledEvent(Guid PlanId) : IDomainEvent;

