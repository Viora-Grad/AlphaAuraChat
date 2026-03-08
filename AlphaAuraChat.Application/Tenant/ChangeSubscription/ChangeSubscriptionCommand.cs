using AlphaAuraChat.Application.Abstractions.Messaging;

namespace AlphaAuraChat.Application.Tenant.ChangeSubscriprion;

public sealed record ChangeSubscriptionCommand(Guid tenantId, Guid plandId) : ICommand;
