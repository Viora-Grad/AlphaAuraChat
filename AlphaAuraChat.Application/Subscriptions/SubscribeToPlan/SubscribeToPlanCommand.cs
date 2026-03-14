
using AlphaAuraChat.Application.Abstractions.Messaging;

namespace AlphaAuraChat.Application.Subscriptions.SubscribeToPlan;

public sealed record SubscribeToPlanCommand(Guid TenantId, Guid PlanId) : ICommand;
