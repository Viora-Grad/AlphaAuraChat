using AlphaAuraChat.Application.Abstractions.Messaging;
using AlphaAuraChat.Domain.Billings;

namespace AlphaAuraChat.Application.Subscriptions.SubscribeToPlan;

public sealed record SubscribeToPlanCommand(Guid TenantId, Guid PlanId) : ICommand<Invoice>;
