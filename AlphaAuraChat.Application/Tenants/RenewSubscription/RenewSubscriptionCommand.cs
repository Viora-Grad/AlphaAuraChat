using AlphaAuraChat.Application.Abstractions.Messaging;

namespace AlphaAuraChat.Application.Tenant.RenewSubscription;

public sealed record RenewSubscriptionCommand(Guid tenantId) : ICommand;
