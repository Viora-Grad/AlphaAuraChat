using AlphaAuraChat.Domain.Abstractions;

namespace AlphaAuraChat.Domain.Billings.Events;

public sealed record InvoicePaidEvent(Guid InvoiceId, Guid ClientId) : IDomainEvent;
