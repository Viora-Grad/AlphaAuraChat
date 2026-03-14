using AlphaAuraChat.Domain.Abstractions;

namespace AlphaAuraChat.Domain.Billings.Events;

public sealed record InvoiceOverdueEvent(Guid InvoiceId, Guid ClientId) : IDomainEvent;

